using Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.DeleteSaleItem;

/// <summary>
/// Unit tests for the <see cref="DeleteSaleItemCommand"/> class.
/// </summary>
public class DeleteSaleItemCommandTest
{
    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Delete SaleItem - Given valid SaleItem ID When validating Then validation succeeds")]
    public void Validate_ValidSaleItemId_ShouldPass()
    {
        // Given
        var command = new DeleteSaleItemCommand(Guid.NewGuid());   

        var validator = new DeleteSaleItemValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Delete SaleItem - Given empty SaleItem ID When validating Then validation fails")]
    public void Validate_EmptySaleItemId_ShouldFail()
    {
        // Given
        var command = new DeleteSaleItemCommand(Guid.Empty);

        var validator = new DeleteSaleItemValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(DeleteSaleItemCommand.Id) &&
            error.ErrorMessage == "SaleItem ID is required");
    }
}