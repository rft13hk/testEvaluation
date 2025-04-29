using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetAllCostumers;

/// <summary>
/// Validator for GetCostumerRequest
/// </summary>
public class GetAllCostumersRequestValidator : AbstractValidator<GetAllCostumersRequest>
{
    /// <summary>
    /// Initializes validation rules for GetCostumerRequest
    /// </summary>
    public GetAllCostumersRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Costumer ID is required");
    }
}
