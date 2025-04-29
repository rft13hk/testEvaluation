using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Costumers.DeleteCostumer;

/// <summary>
/// Validator for DeleteCostumerCommand
/// </summary>
public class DeleteCostumerValidator : AbstractValidator<DeleteCostumerCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteCostumerCommand
    /// </summary>
    public DeleteCostumerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Costumer ID is required");
    }
}
