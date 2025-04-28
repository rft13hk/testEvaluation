using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;

/// <summary>
/// Command to retrieve a paginated list of branches.
/// </summary>
public class GetAllBranchesCommand : IRequest<GetAllBranchesResult>
{
    

    /// <summary>
    /// Gets or sets the page number for pagination (default: 1).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page (default: 10).
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the ordering of results (e.g., "BranchName desc, CreateAt asc").
    /// Default is ascending if not specified.
    /// </summary>
    public string? Order { get; set; }
    
}