using System.Data.Common;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Branch, used to identify in sales which branch the sale occurred from.
/// </summary>
public class Branch: BaseEntity
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
    /// Represents the universal Date/Time of when the Branch was deleted;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; } = null;

    /// <summary>
    /// Indicates which user created this Branch.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;


    #region Navigation Properties

    /// <summary>
    /// Navigation property to the User entities associated with this Branch.
    /// </summary>
    public virtual required User Users { get; set; } 


    /// <summary>
    /// Represents the collection of Sale entities associated with this Branch.
    /// This is a one-to-many relationship, where one Branch can have many Sales.
    /// </summary>
    public virtual ICollection<Sale>? Sales { get; set; }

    #endregion
    
}