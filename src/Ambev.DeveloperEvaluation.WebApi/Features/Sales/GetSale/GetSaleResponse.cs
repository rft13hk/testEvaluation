using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class GetSaleResponse
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

    /// <summary>
        /// Indicates the status of the Sale.
        /// </summary>
        public int TotalItems { get; set; } = 0;

        /// <summary>
        /// Represents the total value of the Sale.
        /// </summary>
        public Decimal TotalValue { get; set; } = 0.0M;

        /// <summary>
        /// Represents the total discount applied to the Sale.
        /// </summary>
        public Decimal TotalDiscount { get; set; } = 0.0M;

        /// <summary>
        /// Represents the total discount applied to the Sale.
        /// </summary>
        public Decimal TotalWithDiscount { get; set; } = 0.0M;
}
