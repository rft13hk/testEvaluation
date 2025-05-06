using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.DeleteSale;

/// <summary>
/// Unit tests for the <see cref="DeleteSaleCommand"/> class.
/// </summary>
public class DeleteSaleCommandTest
{
    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Delete Sale - Given valid Sale ID When validating Then validation succeeds")]
    public void Validate_ValidSaleId_ShouldPass()
    {
        // Given
        var command = new DeleteSaleCommand(Guid.NewGuid());   

        var validator = new DeleteSaleValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Delete Sale - Given empty Sale ID When validating Then validation fails")]
    public void Validate_EmptySaleId_ShouldFail()
    {
        // Given
        var command = new DeleteSaleCommand(Guid.Empty);

        var validator = new DeleteSaleValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(DeleteSaleCommand.Id) &&
            error.ErrorMessage == "Sale ID is required");
    }
}