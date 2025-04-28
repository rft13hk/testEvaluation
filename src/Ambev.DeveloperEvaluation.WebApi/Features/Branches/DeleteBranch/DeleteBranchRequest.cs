namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;

/// <summary>
/// Request model for deleting a Branch
/// </summary>
public class DeleteBranchRequest
{
    /// <summary>
    /// The unique identifier of the Branch to delete
    /// </summary>
    public Guid Id { get; set; }
}
