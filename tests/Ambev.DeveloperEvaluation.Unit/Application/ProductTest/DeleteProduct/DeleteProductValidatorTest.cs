using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.DeleteProduct;

/// <summary>
/// Unit tests for the <see cref="DeleteProductValidator"/> class.
/// </summary>
public class DeleteProductValidatorTests
{
    private readonly DeleteProductValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductValidatorTests"/> class.
    /// </summary>
    public DeleteProductValidatorTests()
    {
        _validator = new DeleteProductValidator();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Delete Product - Given valid Product ID When validating Then validation succeeds")]
    public void Validate_ValidProductId_ShouldPass()
    {
        // Given
        var command = new DeleteProductCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Delete Product - Given empty Product ID When validating Then validation fails")]
    public void Validate_EmptyProductId_ShouldFail()
    {
        // Given
        var command = new DeleteProductCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Product ID is required");
    }
}