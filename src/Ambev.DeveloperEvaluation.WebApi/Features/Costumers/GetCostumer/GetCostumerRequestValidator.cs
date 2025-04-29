using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetCostumer;

/// <summary>
/// Validator for GetCostumerRequest
/// </summary>
public class GetCostumerRequestValidator : AbstractValidator<GetCostumerRequest>
{
    /// <summary>
    /// Initializes validation rules for GetCostumerRequest
    /// </summary>
    public GetCostumerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Costumer ID is required");
    }
}
