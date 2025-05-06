using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale;

/// <summary>
/// Unit tests for the <see cref="GetSaleValidator"/> class.
/// </summary>
public class GetSaleValidatorTests
{
    private readonly GetSaleValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleValidatorTests"/> class.
    /// </summary>
    public GetSaleValidatorTests()
    {
        _validator = new GetSaleValidator();
    }


    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Get Sale - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidSaleId_ShouldPass()
    {
        // Given
        var command = new GetSaleCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Get Sale - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptySaleId_ShouldFail()
    {
        // Given
        var command = new GetSaleCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Sale ID is required");
    }
}