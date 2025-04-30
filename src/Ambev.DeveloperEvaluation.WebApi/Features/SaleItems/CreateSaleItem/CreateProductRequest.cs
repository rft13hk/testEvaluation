using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem;

/// <summary>
/// Represents a request to create a new SaleItem in the system.
/// </summary>
public class CreateSaleItemRequest
{
    /// <summary>
    /// Represents the unique identifier of the Sale.
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid SaleId { get; set; }

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
    /// Indicates which user created this Sale.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;    


}