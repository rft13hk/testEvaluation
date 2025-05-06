using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.GetProduct;

/// <summary>
/// Unit tests for the <see cref="GetProductCommand"/> class.
/// </summary>
public class GetProductCommandTest
{
    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Get Product - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidProductId_ShouldPass()
    {
        // Given
        var command = new GetProductCommand(Guid.NewGuid());
        var validator = new GetProductValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Get Product - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptyProductId_ShouldFail()
    {
        // Given
        var command = new GetProductCommand(Guid.Empty);
        var validator = new GetProductValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(GetProductCommand.Id) &&
            error.ErrorMessage == "Product ID is required");
    }
}