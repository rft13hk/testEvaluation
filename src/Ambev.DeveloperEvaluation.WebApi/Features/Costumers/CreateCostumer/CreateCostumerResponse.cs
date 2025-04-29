using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.CreateCostumer;

/// <summary>
/// API response model for CreateCostumer operation
/// </summary>
public class CreateCostumerResponse
{
        /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }
}
