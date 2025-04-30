namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem;

/// <summary>
/// Request model for getting a SaleItem by ID
/// </summary>
public class GetSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the SaleItem to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
