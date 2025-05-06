using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.CreateProduct;

/// <summary>
/// Unit tests for the <see cref="CreateProductCommand"/> class.
/// </summary>
public class CreateProductCommandTests
{
    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given valid data When validating Then returns valid result")]
    public void Validate_ValidData_ShouldReturnValidResult()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = "Valid Product Name",
            UserId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given empty Product name When validating Then returns invalid result")]
    public void Validate_EmptyProductName_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = string.Empty,
            UserId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then

        validationResult.IsValid.Should().BeFalse();

        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "Product name is required.");
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given empty user ID When validating Then returns invalid result")]
    public void Validate_EmptyUserId_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = "Valid Product Name",
            UserId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "User ID is required.");
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given empty Product name and user ID When validating Then returns multiple errors")]
    public void Validate_EmptyProductNameAndUserId_ShouldReturnMultipleErrors()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = string.Empty,
            UserId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(2);
        validationResult.Errors.Should().Contain(error => error.Detail == "Product name is required.");
        validationResult.Errors.Should().Contain(error => error.Detail == "User ID is required.");
    }
}
