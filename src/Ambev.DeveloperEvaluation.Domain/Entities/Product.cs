using System.Data.Common;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Product, used to identify in sales which Product the sale occurred from.
/// </summary>
public class Product: BaseEntity
{
    
    /// <summary>
    /// Represents the Product code (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets the Product name
    /// Must not be null or empty.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;


    /// <summary>
    /// Represents the Product price;
    /// Must be valid and not null or empty.
    /// </summary>
    public double Price { get; set; } = 0.0;

    /// <summary>
    /// Represents the universal Date/Time of when the Product was created;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the universal Date/Time of when the Product was updated;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset UpdateAt { get; set; } = DateTimeOffset.UtcNow;


    /// <summary>
    /// Represents the universal Date/Time of when the Product was deleted;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; } = null;
    
    /// <summary>
    /// Indicates which user created this Product.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

    #region Navigation Properties

    /// <summary>
    /// Represents the collection of SaleItems associated with this Product.
    /// This is a one-to-many relationship, where one Product can have many SaleItems.
    /// </summary>
    public virtual ICollection<SaleItems>? SaleItems { get; set; }

    /// <summary>
    /// Navigation property to the User entity that created this Product.
    /// Represents a User, used to identify in sales which User the sale occurred from.
    /// </summary>
    public virtual required User User { get; set; }

    #endregion

    
}