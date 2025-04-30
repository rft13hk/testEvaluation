using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

/// <summary>
/// Command for creating a new SaleItem.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a SaleItem,
/// including the SaleItem name and the ID of the user who created it.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="CreateSaleItemResult"/>.
///
/// The data provided in this command is validated using the
/// <see cref="CreateSaleItemValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required rules.
/// </remarks>
public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
{
    /// <summary>
    /// Represents the unique identifier of the Sale.
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Represents the unique identifier of the Product.
    /// Must be valid and not null or empty.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Represents the quantity of the product.
    /// Must be valid and not null or empty.
    /// </summary>
    public int Quantity { get; set; } = 0;  

    /// <summary>
    /// Indicates which user created this Sale.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;    

    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}