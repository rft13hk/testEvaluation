using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Costumers.DeleteCostumer;

/// <summary>
/// Command for deleting a Costumer
/// </summary>
public record DeleteCostumerCommand : IRequest<DeleteCostumerResponse>
{
    /// <summary>
    /// The unique identifier of the Costumer to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteCostumerCommand
    /// </summary>
    /// <param name="id">The ID of the Costumer to delete</param>
    public DeleteCostumerCommand(Guid id)
    {
        Id = id;
    }
}
