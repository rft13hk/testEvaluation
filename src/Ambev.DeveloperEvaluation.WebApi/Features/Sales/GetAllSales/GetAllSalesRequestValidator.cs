using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

/// <summary>
/// Validator for GetSaleRequest
/// </summary>
public class GetAllSalesRequestValidator : AbstractValidator<GetAllSalesRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleRequest
    /// </summary>
    public GetAllSalesRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
