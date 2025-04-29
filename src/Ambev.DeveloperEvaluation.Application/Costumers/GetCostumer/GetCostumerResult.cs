using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;

/// <summary>
/// Response model for GetCostumer operation
/// </summary>
public class GetCostumerResult
{
    /// <summary>
    /// The unique identifier of the Costumer
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

    /// <summary>
    /// Navigation property to the User entity that created this Costumer.
    /// </summary>
    public User CreatedByUser { get; set; } = new User();

}
