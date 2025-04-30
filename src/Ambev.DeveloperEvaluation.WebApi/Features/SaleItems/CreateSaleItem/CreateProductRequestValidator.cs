using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem;

/// <summary>
/// Validator for CreateSaleItemRequest that defines validation rules for SaleItem creation.
/// </summary>
public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - SaleItemname: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// </remarks>
    public CreateSaleItemRequestValidator()
    {
        RuleFor(saleitem => saleitem.SaleId)
            .NotEmpty()
            .WithMessage("Sale ID is required.");

        RuleFor(saleitem => saleitem.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("User ID is required.");
    }
}