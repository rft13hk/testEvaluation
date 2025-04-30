namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

/// <summary>
/// Request model for getting a Sale by ID
/// </summary>
public class GetAllSalesRequest
{
    /// <summary>
    /// The unique identifier of the Sale to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
