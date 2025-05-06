using Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.GetCostumer;

/// <summary>
/// Unit tests for the <see cref="GetCostumerCommand"/> class.
/// </summary>
public class GetCostumerCommandTest
{
    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Get Costumer - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidCostumerId_ShouldPass()
    {
        // Given
        var command = new GetCostumerCommand(Guid.NewGuid());
        var validator = new GetCostumerValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Get Costumer - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptyCostumerId_ShouldFail()
    {
        // Given
        var command = new GetCostumerCommand(Guid.Empty);
        var validator = new GetCostumerValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(GetCostumerCommand.Id) &&
            error.ErrorMessage == "Costumer ID is required");
    }
}