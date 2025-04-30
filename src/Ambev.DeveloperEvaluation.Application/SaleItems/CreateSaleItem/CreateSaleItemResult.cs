namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

/// <summary>
/// Represents the response returned after successfully creating a new SaleItem.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created SaleItem.
/// </remarks>
public class CreateSaleItemResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created SaleItem.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created SaleItem in the system.</value>
    public Guid Id { get; set; }

}