using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new Sale in the system.
/// </summary>
public class CreateSaleRequest
{
/// <summary>
    /// Represents the Sale number (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;
    
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
    /// Indicates which user created this Sale.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

}