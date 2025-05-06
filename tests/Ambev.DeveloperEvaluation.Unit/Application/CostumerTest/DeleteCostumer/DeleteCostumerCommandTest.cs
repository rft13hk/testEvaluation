using Ambev.DeveloperEvaluation.Application.Costumers.DeleteCostumer;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.DeleteCostumer;

/// <summary>
/// Unit tests for the <see cref="DeleteCostumerCommand"/> class.
/// </summary>
public class DeleteCostumerCommandTest
{
    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Delete Costumer - Given valid Costumer ID When validating Then validation succeeds")]
    public void Validate_ValidCostumerId_ShouldPass()
    {
        // Given
        var command = new DeleteCostumerCommand(Guid.NewGuid());   

        var validator = new DeleteCostumerValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Delete Costumer - Given empty Costumer ID When validating Then validation fails")]
    public void Validate_EmptyCostumerId_ShouldFail()
    {
        // Given
        var command = new DeleteCostumerCommand(Guid.Empty);

        var validator = new DeleteCostumerValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(DeleteCostumerCommand.Id) &&
            error.ErrorMessage == "Costumer ID is required");
    }
}