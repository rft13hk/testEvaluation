using Ambev.DeveloperEvaluation.Application.Costumers.DeleteCostumer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.DeleteCostumer;

/// <summary>
/// Unit tests for the <see cref="DeleteCostumerHandler"/> class.
/// </summary>
public class DeleteCostumerHandlerTest
{
    private readonly ICostumerRepository _branchRepository;
    private readonly DeleteCostumerHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCostumerHandlerTest"/> class.
    /// </summary>
    public DeleteCostumerHandlerTest()
    {
        _branchRepository = Substitute.For<ICostumerRepository>();
        _handler = new DeleteCostumerHandler(_branchRepository);
    }


    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Delete Costumer - Given valid branch ID When handling Then deletes branch successfully")]
    public async Task Handle_ValidCostumerId_DeletesCostumerSuccessfully()
    {
        // Given
        var branchId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        _branchRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns(new Ambev.DeveloperEvaluation.Domain.Entities.Costumer() 
            { 
                Id = branchId,
                UserId = userId,
                Users = new Ambev.DeveloperEvaluation.Domain.Entities.User()
                {
                    Id = userId
                } // Initialize the required Users property
            });
            
        _branchRepository.DeleteAsync(branchId, Arg.Any<CancellationToken>())
            .Returns(true);


        // When
        await _handler.Handle(new DeleteCostumerCommand(branchId), CancellationToken.None);

        // Then
        await _branchRepository.Received(1).DeleteAsync(branchId, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Delete Costumer - Given non-existent branch ID When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentCostumerId_ThrowsInvalidOperationException()
    {
        // Given
        var branchId = Guid.NewGuid();
        _branchRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns((Ambev.DeveloperEvaluation.Domain.Entities.Costumer)null!);

        // When
        Func<Task> act = async () => await _handler.Handle(new DeleteCostumerCommand(branchId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Costumer with ID {branchId} not found.");
    }
}