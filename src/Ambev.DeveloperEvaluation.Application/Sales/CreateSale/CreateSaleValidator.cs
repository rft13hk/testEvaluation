using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for Sale creation command.
/// </summary>
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleName: Required, must not be null or empty.
    /// - UserId: Required, must not be an empty Guid.
    /// </remarks>
    public CreateSaleValidator()
    {
        RuleFor(Sale => Sale.SaleNumber)
            .NotEmpty()
            .WithMessage("Sale number is required.");

        RuleFor(Sale => Sale.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("User ID is required.");
    }
}