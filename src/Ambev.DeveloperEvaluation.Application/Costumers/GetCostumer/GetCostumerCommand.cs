using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;

/// <summary>
/// Command for retrieving a Costumer by their ID
/// </summary>
public record GetCostumerCommand : IRequest<GetCostumerResult>
{
    /// <summary>
    /// The unique identifier of the Costumer to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetCostumerCommand
    /// </summary>
    /// <param name="id">The ID of the Costumer to retrieve</param>
    public GetCostumerCommand(Guid id)
    {
        Id = id;
    }
}
