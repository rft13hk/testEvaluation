using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents a request to create a new Product in the system.
/// </summary>
public class CreateProductRequest
{
        /// <summary>
    /// Represents the Product code (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the Product to be created.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Represents the Product price;
    /// Must be valid and not null or empty.
    /// </summary>
    public double Price { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the ID of the user who created this Product.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;


}