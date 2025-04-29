namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.DeleteCostumer;

/// <summary>
/// Request model for deleting a Costumer
/// </summary>
public class DeleteCostumerRequest
{
    /// <summary>
    /// The unique identifier of the Costumer to delete
    /// </summary>
    public Guid Id { get; set; }
}
