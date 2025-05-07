using Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.DeleteSaleItem;

/// <summary>
/// Unit tests for the <see cref="DeleteSaleItemValidator"/> class.
/// </summary>
public class DeleteSaleItemValidatorTests
{
    private readonly DeleteSaleItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleItemValidatorTests"/> class.
    /// </summary>
    public DeleteSaleItemValidatorTests()
    {
        _validator = new DeleteSaleItemValidator();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Delete SaleItem - Given valid SaleItem ID When validating Then validation succeeds")]
    public void Validate_ValidSaleItemId_ShouldPass()
    {
        // Given
        var command = new DeleteSaleItemCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Delete SaleItem - Given empty SaleItem ID When validating Then validation fails")]
    public void Validate_EmptySaleItemId_ShouldFail()
    {
        // Given
        var command = new DeleteSaleItemCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("SaleItem ID is required");
    }
}