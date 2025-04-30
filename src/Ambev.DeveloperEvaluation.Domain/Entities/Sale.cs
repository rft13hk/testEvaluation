using System.Data.Common;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Sale, used to identify in sales which Sale the sale occurred from.
/// </summary>
public class Sale: BaseEntity
{
    /// <summary>
    /// Represents the Sale number (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets the Sale Date
    /// Must not be null or empty.
    /// </summary>
    public DateTimeOffset SaleDate { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the Product code (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid CostumerId { get; set; } = Guid.Empty;

    /// <summary>
    /// Represents the Product code (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid BranchId { get; set; } = Guid.Empty;

    /// <summary>
    /// Represents the universal Date/Time of when the Sale was created;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the universal Date/Time of when the Sale was updated;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset UpdateAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the universal Date/Time of when the Sale was deleted;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; } = null;
    
    /// <summary>
    /// Indicates which user created this Sale.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;


    #region Navigation Properties

    /// <summary>
    /// Navigation property to the Costumer entity that created this Sale.
    /// Represents a Costumer, used to identify in sales which Costumer the sale occurred from.
    /// </summary>
    public virtual required Costumer Costumer { get; set; } 

    /// <summary>
    /// Navigation property to the Branch entity that created this Sale.
    /// Represents a Branch, used to identify in sales which branch the sale occurred from.
    /// This is a one-to-many relationship, where one Branch can have many Sales.
    /// </summary>
    public virtual required Branch Branch { get; set; } 

    /// <summary>
    /// Represents the collection of SaleItems associated with this Sale.
    /// This is a one-to-many relationship, where one Sale can have many SaleItems.
    /// </summary>
    public virtual ICollection<SaleItems>? SaleItems { get; set; }

    /// <summary>
    /// Navigation property to the User entity that created this Sale.
    /// Represents a User, used to identify in sales which User the sale occurred from.
    /// This is a one-to-many relationship, where one User can have many Sales.
    /// </summary>
    public virtual required User User { get; set; } 

    #endregion
    
}