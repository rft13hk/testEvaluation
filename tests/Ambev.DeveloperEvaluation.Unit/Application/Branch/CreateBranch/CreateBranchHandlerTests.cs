using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.CreateBranch;

/// <summary>
/// Unit tests for the <see cref="CreateBranchHandler"/> class.
/// </summary>
public class CreateBranchHandlerTests
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly CreateBranchHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBranchHandlerTests"/> class.
    /// </summary>
    public CreateBranchHandlerTests()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateBranchHandler(_branchRepository, _userRepository, _mapper);
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given valid command When handling Then creates branch successfully")]
    public async Task Handle_ValidCommand_CreatesBranchSuccessfully()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = "Valid Branch",
            UserId = Guid.NewGuid()
        };

        var branch = new Ambev.DeveloperEvaluation.Domain.Entities.Branch
        {
            Id = Guid.NewGuid(),
            BranchName = command.BranchName,
            UserId = command.UserId,
            User = new User { Id = command.UserId },
            CreateAt = DateTimeOffset.UtcNow,
            UpdateAt = DateTimeOffset.UtcNow
        };

        var result = new CreateBranchResult
        {
            Id = branch.Id
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns(new User { Id = command.UserId });
        _mapper.Map<Ambev.DeveloperEvaluation.Domain.Entities.Branch>(command).Returns(branch);
        _branchRepository.CreateAsync(branch, Arg.Any<CancellationToken>()).Returns(branch);
        _mapper.Map<CreateBranchResult>(branch).Returns(result);

        // When
        var createBranchResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createBranchResult.Should().NotBeNull();
        createBranchResult.Id.Should().Be(branch.Id);
        await _branchRepository.Received(1).CreateAsync(branch, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Given
        var command = new CreateBranchCommand(); // Invalid command (empty fields)

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given non-existent user When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentUser_ThrowsInvalidOperationException()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = "Valid Branch",
            UserId = Guid.NewGuid()   
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns((User)null!);

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"User with ID {command.UserId} not found.");
    }
}