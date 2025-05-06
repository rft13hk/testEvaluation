using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.CreateSale;

/// <summary>
/// Unit tests for the <see cref="CreateSaleCommand"/> class.
/// </summary>
public class CreateSaleCommandTests
{
    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given valid data When validating Then returns valid result")]
    public void Validate_ValidData_ShouldReturnValidResult()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = "Valid Sale Name",
            UserId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given empty Sale name When validating Then returns invalid result")]
    public void Validate_EmptySaleNumber_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = string.Empty,
            UserId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then

        validationResult.IsValid.Should().BeFalse();

        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "Sale number is required.");
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given empty user ID When validating Then returns invalid result")]
    public void Validate_EmptyUserId_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = "Valid Sale Number",
            UserId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "User ID is required.");
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given empty Sale name and user ID When validating Then returns multiple errors")]
    public void Validate_EmptySaleNumberAndUserId_ShouldReturnMultipleErrors()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = string.Empty,
            UserId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(2);
        validationResult.Errors.Should().Contain(error => error.Detail == "Sale number is required.");
        validationResult.Errors.Should().Contain(error => error.Detail == "User ID is required.");
    }
}
