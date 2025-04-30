using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem;

/// <summary>
/// API response model for CreateSaleItem operation
/// </summary>
public class GetSaleItemResponse
{
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the SaleItem name
    /// Must not be null or empty.
    /// </summary>
    public string SaleItemName { get; set; } = string.Empty;

    /// <summary>
    /// Represents the universal Date/Time of when the SaleItem was created;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the universal Date/Time of when the SaleItem was updated;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset UpdateAt { get; set; } = DateTimeOffset.UtcNow;
    
    /// <summary>
    /// Indicates which user created this SaleItem.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

}
