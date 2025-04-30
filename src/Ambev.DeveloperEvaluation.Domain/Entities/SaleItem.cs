using System.Data.Common;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Sale, used to identify in sales which Sale the sale occurred from.
/// </summary>
public class SaleItem: BaseEntity
{
    /// <summary>
    /// Represents the unique identifier of the Sale.
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets the Sale Date
    /// Must not be null or empty.
    /// </summary>
    public DateTimeOffset SaleDate { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the unique identifier of the Product.
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// represents the unit price of the product.
    /// Must be valid and not null or empty.
    /// </summary>
    public decimal Price { get; set; } = 0.0m;

    /// <summary>
    /// Represents the quantity of the product.
    /// Must be valid and not null or empty.
    /// </summary>
    public int Quantity { get; set; } = 0;  

    /// <summary>
    /// Represents the discount applied to the product.
    /// Must be valid and not null or empty.
    /// </summary>
    public decimal Discount { get; set; } = 0.0m;

    /// <summary>
    /// Represents the total price of the product.
    /// Must be valid and not null or empty.
    /// </summary>
    public decimal TotalPrice { get; set; } = 0.0m;


    public SaleItemStatus StatusItem { get; set; }

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
    /// Navigation property to the Sale entity that created this Sale.
    /// Represents a Sale, used to identify in sales which Sale the sale occurred from.
    /// </summary>
    public virtual required Sale Sale { get; set; } 

    /// <summary>
    /// Navigation property to the Product entity that created this Sale.
    /// Represents a Product, used to identify in sales which Product the sale occurred from.
    /// </summary>
    public virtual required Product Product { get; set; } 

    /// <summary>
    /// Navigation property to the User entity that created this Sale.
    /// </summary>
    public virtual required User User { get; set; } 
    
    #endregion

    
}