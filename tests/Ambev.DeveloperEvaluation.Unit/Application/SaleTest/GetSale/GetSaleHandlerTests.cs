using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.GetSale;

public class GetSaleHandlerTests
{
    private readonly ISaleRepository _mockRepository;
    private readonly GetSaleHandler _handler;
    //private readonly ISaleRepository _branchRepository;
    private readonly IUserRepository _userRepository;

    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IMapper _mapper;

    public GetSaleHandlerTests()
    {
        // Mock do repositório
        _mockRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _userRepository = Substitute.For<IUserRepository>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _handler = new GetSaleHandler(_mockRepository, _saleItemRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnSaleResult_When_SaleExists()
    {
        // Arrange
        var costumerId = Guid.NewGuid();
        var BrancheId = Guid.NewGuid();
        var user = new Ambev.DeveloperEvaluation.Domain.Entities.User()
        {
            Id = Guid.NewGuid(),
            Username = "User"
        };

        var sale = new Ambev.DeveloperEvaluation.Domain.Entities.Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = "001",
            UserId = user.Id,
            User = user,
            CostumerId = costumerId,
            Costumer = new DeveloperEvaluation.Domain.Entities.Costumer()
            {
                Id = costumerId,
                UserId = user.Id,
                Users = user
            },
            BranchId = BrancheId,
            Branch = new DeveloperEvaluation.Domain.Entities.Branch()
            {
                Id = BrancheId,
                UserId = user.Id,
                User = user
            },
            CreateAt = DateTimeOffset.UtcNow,
            UpdateAt = DateTimeOffset.UtcNow
        };

        var resultSale = new GetSaleResult
        {
            Id = sale.Id
            , SaleNumber = sale.SaleNumber
        };

        _mockRepository.GetByIdAsync(sale.Id)!.Returns(Task.FromResult(sale));
        _mapper.Map<GetSaleResult>(sale).Returns(resultSale);        
        var query = new GetSaleCommand(sale.Id);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(sale.Id, result.Id);
        Assert.Equal(sale.SaleNumber, result.SaleNumber);

    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_SaleDoesNotExist()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        _mockRepository.GetByIdAsync(branchId)!.Returns(Task.FromResult<Ambev.DeveloperEvaluation.Domain.Entities.Sale>(null!));

        var query = new GetSaleCommand(branchId);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }


}
