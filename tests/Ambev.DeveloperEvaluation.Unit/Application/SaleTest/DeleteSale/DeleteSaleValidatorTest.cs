using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.DeleteSale;

/// <summary>
/// Unit tests for the <see cref="DeleteSaleValidator"/> class.
/// </summary>
public class DeleteSaleValidatorTests
{
    private readonly DeleteSaleValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleValidatorTests"/> class.
    /// </summary>
    public DeleteSaleValidatorTests()
    {
        _validator = new DeleteSaleValidator();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Delete Sale - Given valid Sale ID When validating Then validation succeeds")]
    public void Validate_ValidSaleId_ShouldPass()
    {
        // Given
        var command = new DeleteSaleCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Delete Sale - Given empty Sale ID When validating Then validation fails")]
    public void Validate_EmptySaleId_ShouldFail()
    {
        // Given
        var command = new DeleteSaleCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Sale ID is required");
    }
}