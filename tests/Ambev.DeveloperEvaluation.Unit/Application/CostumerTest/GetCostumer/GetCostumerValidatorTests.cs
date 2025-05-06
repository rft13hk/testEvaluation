using Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Costumer;

/// <summary>
/// Unit tests for the <see cref="GetCostumerValidator"/> class.
/// </summary>
public class GetCostumerValidatorTests
{
    private readonly GetCostumerValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCostumerValidatorTests"/> class.
    /// </summary>
    public GetCostumerValidatorTests()
    {
        _validator = new GetCostumerValidator();
    }


    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Get Costumer - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidCostumerId_ShouldPass()
    {
        // Given
        var command = new GetCostumerCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Get Costumer - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptyCostumerId_ShouldFail()
    {
        // Given
        var command = new GetCostumerCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Costumer ID is required");
    }
}