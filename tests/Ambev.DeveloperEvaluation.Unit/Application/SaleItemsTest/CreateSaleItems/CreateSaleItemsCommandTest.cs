using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.CreateSaleItem;

/// <summary>
/// Unit tests for the <see cref="CreateSaleItemCommand"/> class.
/// </summary>
public class CreateSaleItemCommandTests
{
    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given valid data When validating Then returns valid result")]
    public void Validate_ValidData_ShouldReturnValidResult()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given empty SaleItem name When validating Then returns invalid result")]
    public void Validate_EmptySaleItemNumber_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            SaleId = Guid.Empty,
            UserId = Guid.Empty,
            ProductId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then

        validationResult.IsValid.Should().BeFalse();

        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "Product ID is required.");
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given empty user ID When validating Then returns invalid result")]
    public void Validate_EmptyUserId_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            SaleId = Guid.Empty,
            UserId = Guid.Empty,
            ProductId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "User ID is required.");
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given empty SaleItem name and user ID When validating Then returns multiple errors")]
    public void Validate_EmptySaleItemNumberAndUserId_ShouldReturnMultipleErrors()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            SaleId = Guid.Empty,
            UserId = Guid.Empty,
            ProductId = Guid.Empty
        };


        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(2);
        validationResult.Errors.Should().Contain(error => error.Detail == "Product ID is required.");
        validationResult.Errors.Should().Contain(error => error.Detail == "User ID is required.");
    }
}
