namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Represents the response returned after successfully creating a new branch.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created branch.
/// </remarks>
public class CreateBranchResult
{
    /// <summary>
    /// Gets the Branch name
    /// Must not be null or empty.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Represents the universal Date/Time of when the Branch was created;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the universal Date/Time of when the Branch was updated;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset UpdateAt { get; set; } = DateTimeOffset.UtcNow;
    
    /// <summary>
    /// Indicates which user created this Branch.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

}