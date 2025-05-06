using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.GetProduct;

public class GetProductHandlerTests
{
    private readonly IProductRepository _mockRepository;
    private readonly GetProductHandler _handler;
    //private readonly IProductRepository _branchRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetProductHandlerTests()
    {
        // Mock do repositório
        _mockRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _userRepository = Substitute.For<IUserRepository>();
        _handler = new GetProductHandler(_mockRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnProductResult_When_ProductExists()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        var branch = new Ambev.DeveloperEvaluation.Domain.Entities.Product
        {
            Id = branchId,
            ProductName = "Test Product",
            UserId = Guid.NewGuid(),
            User = new User
            {
                Id = Guid.NewGuid(),
                Username = "Test User"
            },
            CreateAt = DateTimeOffset.UtcNow,
            UpdateAt = DateTimeOffset.UtcNow
        };

        var resultProduct = new GetProductResult
        {
            Id = branch.Id
            , ProductName = branch.ProductName
        };

        _mockRepository.GetByIdAsync(branchId)!.Returns(Task.FromResult(branch));
        _mapper.Map<GetProductResult>(branch).Returns(resultProduct);        
        var query = new GetProductCommand(branchId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(branch.Id, result.Id);
        Assert.Equal(branch.ProductName, result.ProductName);

    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_ProductDoesNotExist()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        _mockRepository.GetByIdAsync(branchId)!.Returns(Task.FromResult<Ambev.DeveloperEvaluation.Domain.Entities.Product>(null!));

        var query = new GetProductCommand(branchId);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }


}
