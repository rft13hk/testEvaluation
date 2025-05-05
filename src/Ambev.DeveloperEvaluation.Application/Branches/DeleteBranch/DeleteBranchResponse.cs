using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;

/// <summary>
/// Response model for DeleteBranch operation
/// </summary>
[ExcludeFromCodeCoverage]
public class DeleteBranchResponse
{
    /// <summary>
    /// Indicates whether the deletion was successful
    /// </summary>
    public bool Success { get; set; }
}
