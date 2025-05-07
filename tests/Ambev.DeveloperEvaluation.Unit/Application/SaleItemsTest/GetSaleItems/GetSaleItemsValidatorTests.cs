using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItem;

/// <summary>
/// Unit tests for the <see cref="GetSaleItemValidator"/> class.
/// </summary>
public class GetSaleItemValidatorTests
{
    private readonly GetSaleItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleItemValidatorTests"/> class.
    /// </summary>
    public GetSaleItemValidatorTests()
    {
        _validator = new GetSaleItemValidator();
    }


    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Get SaleItem - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidSaleItemId_ShouldPass()
    {
        // Given
        var command = new GetSaleItemCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Get SaleItem - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptySaleItemId_ShouldFail()
    {
        // Given
        var command = new GetSaleItemCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("SaleItem ID is required");
    }
}