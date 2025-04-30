using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem;

/// <summary>
/// API response model for CreateSaleItem operation
/// </summary>
public class CreateSaleItemResponse
{
        /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }
}
