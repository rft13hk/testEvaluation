using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.CreateSaleItem;

/// <summary>
/// Unit tests for the <see cref="CreateSaleItemValidator"/> class.
/// </summary>
public class CreateSaleItemValidatorTests
{
    private readonly CreateSaleItemValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleItemValidatorTests"/> class.
    /// </summary>
    public CreateSaleItemValidatorTests()
    {
        _validator = new CreateSaleItemValidator();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given valid data When validating Then validation succeeds")]
    public void Validate_ValidData_ShouldPass()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            UserId = Guid.NewGuid()
            , ProductId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given empty branch name When validating Then validation fails")]
    public void Validate_EmptySaleItemNumber_ShouldFail()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.ProductId)
            .WithErrorMessage("Product ID is required.");
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given empty user ID When validating Then validation fails")]
    public void Validate_EmptyUserId_ShouldFail()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            ProductId = Guid.Empty,
            SaleId = Guid.Empty,
            UserId = Guid.Empty
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.UserId)
            .WithErrorMessage("User ID is required.");
    }
}