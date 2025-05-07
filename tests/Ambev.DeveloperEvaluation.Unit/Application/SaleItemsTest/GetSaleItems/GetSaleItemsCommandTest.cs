using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.GetSaleItem;

/// <summary>
/// Unit tests for the <see cref="GetSaleItemCommand"/> class.
/// </summary>
public class GetSaleItemCommandTest
{
    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Get SaleItem - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidSaleItemId_ShouldPass()
    {
        // Given
        var command = new GetSaleItemCommand(Guid.NewGuid());
        var validator = new GetSaleItemValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Get SaleItem - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptySaleItemId_ShouldFail()
    {
        // Given
        var command = new GetSaleItemCommand(Guid.Empty);
        var validator = new GetSaleItemValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(GetSaleItemCommand.Id) &&
            error.ErrorMessage == "SaleItem ID is required");
    }
}