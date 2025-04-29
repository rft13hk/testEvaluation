using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.CreateCostumer;

/// <summary>
/// Validator for CreateCostumerRequest that defines validation rules for Costumer creation.
/// </summary>
public class CreateCostumerRequestValidator : AbstractValidator<CreateCostumerRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCostumerRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Costumername: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// </remarks>
    public CreateCostumerRequestValidator()
    {
        RuleFor(costumer => costumer.CostumerName)
            .NotEmpty()
            .WithMessage("Costumer name is required.");

        RuleFor(costumer => costumer.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("User ID is required.");
    }
}