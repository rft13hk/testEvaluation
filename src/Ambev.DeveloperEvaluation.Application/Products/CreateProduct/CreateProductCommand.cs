using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new Product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a Product,
/// including the Product name and the ID of the user who created it.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="CreateProductResult"/>.
///
/// The data provided in this command is validated using the
/// <see cref="CreateProductValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required rules.
/// </remarks>
public class CreateProductCommand : IRequest<CreateProductResult>
{
    /// <summary>
    /// Represents the Product code (SKU).
    /// Must be valid and not null or empty.
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the Product to be created.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Represents the Product price;
    /// Must be valid and not null or empty.
    /// </summary>
    public double Price { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the ID of the user who created this Product.
    /// </summary>
    public Guid UserId { get; set; } = Guid.Empty;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}