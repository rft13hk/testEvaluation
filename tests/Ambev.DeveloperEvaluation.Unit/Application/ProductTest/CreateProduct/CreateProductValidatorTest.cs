using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.CreateProduct;

/// <summary>
/// Unit tests for the <see cref="CreateProductValidator"/> class.
/// </summary>
public class CreateProductValidatorTests
{
    private readonly CreateProductValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductValidatorTests"/> class.
    /// </summary>
    public CreateProductValidatorTests()
    {
        _validator = new CreateProductValidator();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given valid data When validating Then validation succeeds")]
    public void Validate_ValidData_ShouldPass()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = "Valid Product Name",
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given empty branch name When validating Then validation fails")]
    public void Validate_EmptyProductName_ShouldFail()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = string.Empty,
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.ProductName)
            .WithErrorMessage("Product name is required.");
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given empty user ID When validating Then validation fails")]
    public void Validate_EmptyUserId_ShouldFail()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = "Valid Product Name",
            UserId = Guid.Empty
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.UserId)
            .WithErrorMessage("User ID is required.");
    }
}