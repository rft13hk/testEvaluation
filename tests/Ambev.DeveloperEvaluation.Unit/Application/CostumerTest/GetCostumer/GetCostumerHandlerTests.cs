using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.GetCostumer;

public class GetCostumerHandlerTests
{
    private readonly ICostumerRepository _mockRepository;
    private readonly GetCostumerHandler _handler;
    //private readonly ICostumerRepository _branchRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetCostumerHandlerTests()
    {
        // Mock do repositório
        _mockRepository = Substitute.For<ICostumerRepository>();
        _mapper = Substitute.For<IMapper>();
        _userRepository = Substitute.For<IUserRepository>();
        _handler = new GetCostumerHandler(_mockRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnCostumerResult_When_CostumerExists()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        var branch = new Ambev.DeveloperEvaluation.Domain.Entities.Costumer
        {
            Id = branchId,
            CostumerName = "Test Costumer",
            UserId = Guid.NewGuid(),
            Users = new User
            {
                Id = Guid.NewGuid(),
                Username = "Test User"
            },
            CreateAt = DateTimeOffset.UtcNow,
            UpdateAt = DateTimeOffset.UtcNow
        };

        var resultCostumer = new GetCostumerResult
        {
            Id = branch.Id
            , CostumerName = branch.CostumerName
        };

        _mockRepository.GetByIdAsync(branchId)!.Returns(Task.FromResult(branch));
        _mapper.Map<GetCostumerResult>(branch).Returns(resultCostumer);        
        var query = new GetCostumerCommand(branchId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(branch.Id, result.Id);
        Assert.Equal(branch.CostumerName, result.CostumerName);

    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_CostumerDoesNotExist()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        _mockRepository.GetByIdAsync(branchId)!.Returns(Task.FromResult<Ambev.DeveloperEvaluation.Domain.Entities.Costumer>(null!));

        var query = new GetCostumerCommand(branchId);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }


}
