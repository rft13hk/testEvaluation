namespace Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;

/// <summary>
/// Represents the response returned after successfully creating a new Costumer.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created Costumer.
/// </remarks>
public class CreateCostumerResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created Costumer.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created Costumer in the system.</value>
    public Guid Id { get; set; }

}