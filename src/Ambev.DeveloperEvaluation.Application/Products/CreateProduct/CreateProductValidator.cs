using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for Product creation command.
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ProductName: Required, must not be null or empty.
    /// - UserId: Required, must not be an empty Guid.
    /// </remarks>
    public CreateProductValidator()
    {
        RuleFor(Product => Product.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(Product => Product.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("User ID is required.");
    }
}