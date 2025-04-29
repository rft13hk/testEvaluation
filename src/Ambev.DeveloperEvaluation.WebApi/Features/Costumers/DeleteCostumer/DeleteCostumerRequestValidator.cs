using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.DeleteCostumer;

/// <summary>
/// Validator for DeleteCostumerRequest
/// </summary>
public class DeleteCostumerRequestValidator : AbstractValidator<DeleteCostumerRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteCostumerRequest
    /// </summary>
    public DeleteCostumerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Costumer ID is required");
    }
}
