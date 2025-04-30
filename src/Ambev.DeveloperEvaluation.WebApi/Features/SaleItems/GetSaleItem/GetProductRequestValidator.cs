using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem;

/// <summary>
/// Validator for GetSaleItemRequest
/// </summary>
public class GetSaleItemRequestValidator : AbstractValidator<GetSaleItemRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleItemRequest
    /// </summary>
    public GetSaleItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
