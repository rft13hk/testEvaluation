using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

/// <summary>
/// Validator for CreateSaleItemCommand that defines validation rules for SaleItem creation command.
/// </summary>
public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleItemCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleItemName: Required, must not be null or empty.
    /// - UserId: Required, must not be an empty Guid.
    /// </remarks>
    public CreateSaleItemValidator()
    {
        RuleFor(SaleItem => SaleItem.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(SaleItem => SaleItem.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("User ID is required.");
    }
}