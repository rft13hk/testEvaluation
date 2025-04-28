using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;

/// <summary>
/// Command for deleting a Branch
/// </summary>
public record DeleteBranchCommand : IRequest<DeleteBranchResponse>
{
    /// <summary>
    /// The unique identifier of the Branch to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteBranchCommand
    /// </summary>
    /// <param name="id">The ID of the Branch to delete</param>
    public DeleteBranchCommand(Guid id)
    {
        Id = id;
    }
}
