namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetAllBranches;

/// <summary>
/// Request model for getting a Branch by ID
/// </summary>
public class GetAllBranchesRequest
{
    /// <summary>
    /// The unique identifier of the Branch to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
