using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.CreateSale;

/// <summary>
/// Unit tests for the <see cref="CreateSaleValidator"/> class.
/// </summary>
public class CreateSaleValidatorTests
{
    private readonly CreateSaleValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleValidatorTests"/> class.
    /// </summary>
    public CreateSaleValidatorTests()
    {
        _validator = new CreateSaleValidator();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given valid data When validating Then validation succeeds")]
    public void Validate_ValidData_ShouldPass()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = "Valid Sale Name",
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given empty branch name When validating Then validation fails")]
    public void Validate_EmptySaleNumber_ShouldFail()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = string.Empty,
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.SaleNumber)
            .WithErrorMessage("Sale number is required.");
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given empty user ID When validating Then validation fails")]
    public void Validate_EmptyUserId_ShouldFail()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = "Valid Sale Name",
            UserId = Guid.Empty
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.UserId)
            .WithErrorMessage("User ID is required.");
    }
}