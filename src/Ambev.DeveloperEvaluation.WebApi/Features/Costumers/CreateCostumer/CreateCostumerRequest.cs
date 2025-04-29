using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.CreateCostumer;

/// <summary>
/// Represents a request to create a new Costumer in the system.
/// </summary>
public class CreateCostumerRequest
{
    /// <summary>
    /// Represents the CPF (Cadastro de Pessoas Físicas) of the Costumer.
    /// Must be valid and not null or empty.
    /// </summary>
    public string CPF { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets the Costumer name
    /// Must not be null or empty.
    /// </summary>
    public string CostumerName { get; set; } = string.Empty;
    
    /// <summary>
    /// Indicates which user created this Costumer.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

}