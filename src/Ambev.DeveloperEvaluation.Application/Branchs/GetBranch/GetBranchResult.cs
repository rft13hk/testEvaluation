using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;

/// <summary>
/// Response model for GetBranch operation
/// </summary>
public class GetBranchResult
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

    /// <summary>
    /// Navigation property to the User entity that created this Branch.
    /// </summary>
    public User CreatedByUser { get; set; } = new User();

}
