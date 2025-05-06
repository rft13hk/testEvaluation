using Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.CreateCostumer;

/// <summary>
/// Unit tests for the <see cref="CreateCostumerValidator"/> class.
/// </summary>
public class CreateCostumerValidatorTests
{
    private readonly CreateCostumerValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCostumerValidatorTests"/> class.
    /// </summary>
    public CreateCostumerValidatorTests()
    {
        _validator = new CreateCostumerValidator();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given valid data When validating Then validation succeeds")]
    public void Validate_ValidData_ShouldPass()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = "Valid Costumer Name",
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given empty branch name When validating Then validation fails")]
    public void Validate_EmptyCostumerName_ShouldFail()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = string.Empty,
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.CostumerName)
            .WithErrorMessage("Costumer name is required.");
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given empty user ID When validating Then validation fails")]
    public void Validate_EmptyUserId_ShouldFail()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = "Valid Costumer Name",
            UserId = Guid.Empty
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.UserId)
            .WithErrorMessage("User ID is required.");
    }
}