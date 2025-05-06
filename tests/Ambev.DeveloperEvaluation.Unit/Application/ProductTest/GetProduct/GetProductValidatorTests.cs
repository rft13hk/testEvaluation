using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product;

/// <summary>
/// Unit tests for the <see cref="GetProductValidator"/> class.
/// </summary>
public class GetProductValidatorTests
{
    private readonly GetProductValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductValidatorTests"/> class.
    /// </summary>
    public GetProductValidatorTests()
    {
        _validator = new GetProductValidator();
    }


    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Get Product - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidProductId_ShouldPass()
    {
        // Given
        var command = new GetProductCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Get Product - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptyProductId_ShouldFail()
    {
        // Given
        var command = new GetProductCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Product ID is required");
    }
}