using System.Data.Common;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Costumer, used to identify in sales which Costumer the sale occurred from.
/// </summary>
public class Costumer: BaseEntity
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
    /// Represents the universal Date/Time of when the Costumer was deleted;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; } = null;
    
    /// <summary>
    /// Indicates which user created this Costumer.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

    /// <summary>
    /// Navigation property to the User entity that created this Costumer.
    /// </summary>
    public User CreatedByUser { get; set; } = new User();

    
}