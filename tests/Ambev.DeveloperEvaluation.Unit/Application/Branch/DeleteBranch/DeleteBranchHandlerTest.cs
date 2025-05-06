using Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.DeleteBranch;

/// <summary>
/// Unit tests for the <see cref="DeleteBranchHandler"/> class.
/// </summary>
public class DeleteBranchHandlerTest
{
    private readonly IBranchRepository _branchRepository;
    private readonly DeleteBranchHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBranchHandlerTest"/> class.
    /// </summary>
    public DeleteBranchHandlerTest()
    {
        _branchRepository = Substitute.For<IBranchRepository>();
        _handler = new DeleteBranchHandler(_branchRepository);
    }


    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Delete Branch - Given valid branch ID When handling Then deletes branch successfully")]
    public async Task Handle_ValidBranchId_DeletesBranchSuccessfully()
    {
        // Given
        var branchId = Guid.NewGuid();
        _branchRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns(new Ambev.DeveloperEvaluation.Domain.Entities.Branch 
            { 
                Id = branchId,
                UserId = Guid.NewGuid(),
                User = new User { Id = Guid.NewGuid() },
            });
        _branchRepository.DeleteAsync(branchId, Arg.Any<CancellationToken>())
            .Returns(true);


        // When
        await _handler.Handle(new DeleteBranchCommand(branchId), CancellationToken.None);

        // Then
        await _branchRepository.Received(1).DeleteAsync(branchId, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Delete Branch - Given non-existent branch ID When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentBranchId_ThrowsInvalidOperationException()
    {
        // Given
        var branchId = Guid.NewGuid();
        _branchRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns((Ambev.DeveloperEvaluation.Domain.Entities.Branch)null!);

        // When
        Func<Task> act = async () => await _handler.Handle(new DeleteBranchCommand(branchId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Branch with ID {branchId} not found.");
    }
}