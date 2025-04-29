namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// Request model for getting a Product by ID
/// </summary>
public class GetAllProductsRequest
{
    /// <summary>
    /// The unique identifier of the Product to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
