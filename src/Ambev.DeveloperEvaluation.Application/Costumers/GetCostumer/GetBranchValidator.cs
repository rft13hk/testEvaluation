using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
public class GetCostumerValidator : AbstractValidator<GetCostumerCommand>
{
    /// <summary>
    /// Initializes validation rules for GetCostumerCommand
    /// </summary>
    public GetCostumerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Costumer ID is required");
    }
}
