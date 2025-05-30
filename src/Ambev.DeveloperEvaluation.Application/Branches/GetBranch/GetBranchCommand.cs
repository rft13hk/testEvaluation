using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

/// <summary>
/// Command for retrieving a Branch by their ID
/// </summary>
public record GetBranchCommand : IRequest<GetBranchResult>
{
    /// <summary>
    /// The unique identifier of the Branch to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetBranchCommand
    /// </summary>
    /// <param name="id">The ID of the Branch to retrieve</param>
    public GetBranchCommand(Guid id)
    {
        Id = id;
    }
}
