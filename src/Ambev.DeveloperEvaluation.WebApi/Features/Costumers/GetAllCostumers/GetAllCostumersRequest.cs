namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetAllCostumers;

/// <summary>
/// Request model for getting a Costumer by ID
/// </summary>
public class GetAllCostumersRequest
{
    /// <summary>
    /// The unique identifier of the Costumer to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
