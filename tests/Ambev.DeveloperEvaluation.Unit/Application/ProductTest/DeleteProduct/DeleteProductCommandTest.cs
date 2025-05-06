using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.DeleteProduct;

/// <summary>
/// Unit tests for the <see cref="DeleteProductCommand"/> class.
/// </summary>
public class DeleteProductCommandTest
{
    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Delete Product - Given valid Product ID When validating Then validation succeeds")]
    public void Validate_ValidProductId_ShouldPass()
    {
        // Given
        var command = new DeleteProductCommand(Guid.NewGuid());   

        var validator = new DeleteProductValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Delete Product - Given empty Product ID When validating Then validation fails")]
    public void Validate_EmptyProductId_ShouldFail()
    {
        // Given
        var command = new DeleteProductCommand(Guid.Empty);

        var validator = new DeleteProductValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(DeleteProductCommand.Id) &&
            error.ErrorMessage == "Product ID is required");
    }
}