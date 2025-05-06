using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch;

/// <summary>
/// Unit tests for the <see cref="GetBranchValidator"/> class.
/// </summary>
public class GetBranchValidatorTests
{
    private readonly GetBranchValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBranchValidatorTests"/> class.
    /// </summary>
    public GetBranchValidatorTests()
    {
        _validator = new GetBranchValidator();
    }


    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Get Branch - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidBranchId_ShouldPass()
    {
        // Given
        var command = new GetBranchCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Get Branch - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptyBranchId_ShouldFail()
    {
        // Given
        var command = new GetBranchCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Branch ID is required");
    }
}