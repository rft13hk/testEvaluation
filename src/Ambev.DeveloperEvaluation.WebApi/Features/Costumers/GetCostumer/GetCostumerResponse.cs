using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetCostumer;

/// <summary>
/// API response model for CreateCostumer operation
/// </summary>
public class GetCostumerResponse
{
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the Costumer name
    /// Must not be null or empty.
    /// </summary>
    public string CostumerName { get; set; } = string.Empty;

    /// <summary>
    /// Represents the universal Date/Time of when the Costumer was created;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the universal Date/Time of when the Costumer was updated;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset UpdateAt { get; set; } = DateTimeOffset.UtcNow;
    
    /// <summary>
    /// Indicates which user created this Costumer.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

}
