namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetAllSaleItems;

/// <summary>
/// Request model for getting a SaleItem by ID
/// </summary>
public class GetAllSaleItemsRequest
{
    /// <summary>
    /// The unique identifier of the SaleItem to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
