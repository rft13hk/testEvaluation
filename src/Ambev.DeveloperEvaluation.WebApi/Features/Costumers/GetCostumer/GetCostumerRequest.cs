namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetCostumer;

/// <summary>
/// Request model for getting a Costumer by ID
/// </summary>
public class GetCostumerRequest
{
    /// <summary>
    /// The unique identifier of the Costumer to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
