using Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.CreateCostumer;

/// <summary>
/// Unit tests for the <see cref="CreateCostumerCommand"/> class.
/// </summary>
public class CreateCostumerCommandTests
{
    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given valid data When validating Then returns valid result")]
    public void Validate_ValidData_ShouldReturnValidResult()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = "Valid Costumer Name",
            UserId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given empty Costumer name When validating Then returns invalid result")]
    public void Validate_EmptyCostumerName_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = string.Empty,
            UserId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then

        validationResult.IsValid.Should().BeFalse();

        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "Costumer name is required.");
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given empty user ID When validating Then returns invalid result")]
    public void Validate_EmptyUserId_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = "Valid Costumer Name",
            UserId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "User ID is required.");
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given empty Costumer name and user ID When validating Then returns multiple errors")]
    public void Validate_EmptyCostumerNameAndUserId_ShouldReturnMultipleErrors()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = string.Empty,
            UserId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(2);
        validationResult.Errors.Should().Contain(error => error.Detail == "Costumer name is required.");
        validationResult.Errors.Should().Contain(error => error.Detail == "User ID is required.");
    }
}
