using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.GetBranch;

public class GetBranchHandlerTests
{
    private readonly IBranchRepository _mockRepository;
    private readonly GetBranchHandler _handler;
    //private readonly IBranchRepository _branchRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetBranchHandlerTests()
    {
        // Mock do repositório
        _mockRepository = Substitute.For<IBranchRepository>();
        _mapper = Substitute.For<IMapper>();
        _userRepository = Substitute.For<IUserRepository>();
        _handler = new GetBranchHandler(_mockRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnBranchResult_When_BranchExists()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        var branch = new Ambev.DeveloperEvaluation.Domain.Entities.Branch
        {
            Id = branchId,
            BranchName = "Test Branch",
            UserId = Guid.NewGuid(),
            User = new User
            {
                Id = Guid.NewGuid(),
                Username = "Test User"
            },
            CreateAt = DateTimeOffset.UtcNow,
            UpdateAt = DateTimeOffset.UtcNow
        };

        var resultBranch = new GetBranchResult
        {
            Id = branch.Id
            , BranchName = branch.BranchName
        };

        _mockRepository.GetByIdAsync(branchId)!.Returns(Task.FromResult(branch));
        _mapper.Map<GetBranchResult>(branch).Returns(resultBranch);        
        var query = new GetBranchCommand(branchId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(branch.Id, result.Id);
        Assert.Equal(branch.BranchName, result.BranchName);

    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_BranchDoesNotExist()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        _mockRepository.GetByIdAsync(branchId)!.Returns(Task.FromResult<Ambev.DeveloperEvaluation.Domain.Entities.Branch>(null!));

        var query = new GetBranchCommand(branchId);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Should_CallRepository_WithCorrectParameters()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        var query = new GetBranchCommand(branchId);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        await _mockRepository.Received(1).GetByIdAsync(branchId);
    }
}
