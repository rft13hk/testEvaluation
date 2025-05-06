using Ambev.DeveloperEvaluation.Application.Costumers.DeleteCostumer;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.DeleteCostumer;

/// <summary>
/// Unit tests for the <see cref="DeleteCostumerValidator"/> class.
/// </summary>
public class DeleteCostumerValidatorTests
{
    private readonly DeleteCostumerValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCostumerValidatorTests"/> class.
    /// </summary>
    public DeleteCostumerValidatorTests()
    {
        _validator = new DeleteCostumerValidator();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Delete Costumer - Given valid Costumer ID When validating Then validation succeeds")]
    public void Validate_ValidCostumerId_ShouldPass()
    {
        // Given
        var command = new DeleteCostumerCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Delete Costumer - Given empty Costumer ID When validating Then validation fails")]
    public void Validate_EmptyCostumerId_ShouldFail()
    {
        // Given
        var command = new DeleteCostumerCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Costumer ID is required");
    }
}