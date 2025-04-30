namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.DeleteSaleItem;

/// <summary>
/// Request model for deleting a SaleItem
/// </summary>
public class DeleteSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the SaleItem to delete
    /// </summary>
    public Guid Id { get; set; }
}
