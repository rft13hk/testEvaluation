using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.GetSale;

/// <summary>
/// Unit tests for the <see cref="GetSaleCommand"/> class.
/// </summary>
public class GetSaleCommandTest
{
    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Get Sale - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidSaleId_ShouldPass()
    {
        // Given
        var command = new GetSaleCommand(Guid.NewGuid());
        var validator = new GetSaleValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Get Sale - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptySaleId_ShouldFail()
    {
        // Given
        var command = new GetSaleCommand(Guid.Empty);
        var validator = new GetSaleValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(GetSaleCommand.Id) &&
            error.ErrorMessage == "Sale ID is required");
    }
}