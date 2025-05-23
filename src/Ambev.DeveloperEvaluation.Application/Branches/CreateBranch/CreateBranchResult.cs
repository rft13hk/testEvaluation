using System.Diagnostics.CodeAnalysis;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Represents the response returned after successfully creating a new branch.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created branch.
/// </remarks>
[ExcludeFromCodeCoverage]
public class CreateBranchResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Branch.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created branch in the system.</value>
    public Guid Id { get; set; }

}