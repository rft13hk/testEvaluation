using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.DeleteSaleItem;

/// <summary>
/// Validator for DeleteSaleItemRequest
/// </summary>
public class DeleteSaleItemRequestValidator : AbstractValidator<DeleteSaleItemRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteSaleItemRequest
    /// </summary>
    public DeleteSaleItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("SaleItem ID is required");
    }
}
