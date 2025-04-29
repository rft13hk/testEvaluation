using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;

/// <summary>
/// Validator for CreateCostumerCommand that defines validation rules for Costumer creation command.
/// </summary>
public class CreateCostumerValidator : AbstractValidator<CreateCostumerCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCostumerCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CostumerName: Required, must not be null or empty.
    /// - UserId: Required, must not be an empty Guid.
    /// </remarks>
    public CreateCostumerValidator()
    {
        RuleFor(Costumer => Costumer.CostumerName)
            .NotEmpty()
            .WithMessage("Costumer name is required.");

        RuleFor(Costumer => Costumer.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("User ID is required.");
    }
}