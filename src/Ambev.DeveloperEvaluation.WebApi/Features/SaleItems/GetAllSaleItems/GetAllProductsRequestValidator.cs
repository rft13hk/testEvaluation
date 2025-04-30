using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetAllSaleItems;

/// <summary>
/// Validator for GetSaleItemRequest
/// </summary>
public class GetAllSaleItemsRequestValidator : AbstractValidator<GetAllSaleItemsRequest>
{
    /// <summary>
    /// Initializes validation rules for GetSaleItemRequest
    /// </summary>
    public GetAllSaleItemsRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
