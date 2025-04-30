using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new Sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a Sale,
/// including the Sale name and the ID of the user who created it.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="CreateSaleResult"/>.
///
/// The data provided in this command is validated using the
/// <see cref="CreateSaleValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required rules.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
{
/// <summary>
    /// Represents the Sale number (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets the Sale Date
    /// Must not be null or empty.
    /// </summary>
    public DateTimeOffset SaleDate { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the Product code (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid CostumerId { get; set; } = Guid.Empty;

    /// <summary>
    /// Represents the Product code (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid BranchId { get; set; } = Guid.Empty;

    /// <summary>
    /// Represents the universal Date/Time of when the Sale was created;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the universal Date/Time of when the Sale was updated;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset UpdateAt { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Represents the universal Date/Time of when the Sale was deleted;
    /// If not informed, assumes the universal date/time of where the system is running;
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; } = null;
    
    /// <summary>
    /// Indicates which user created this Sale.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}