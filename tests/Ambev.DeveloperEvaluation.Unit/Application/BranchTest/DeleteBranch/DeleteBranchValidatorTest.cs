using Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.DeleteBranch;

/// <summary>
/// Unit tests for the <see cref="DeleteBranchValidator"/> class.
/// </summary>
public class DeleteBranchValidatorTests
{
    private readonly DeleteBranchValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBranchValidatorTests"/> class.
    /// </summary>
    public DeleteBranchValidatorTests()
    {
        _validator = new DeleteBranchValidator();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Delete Branch - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidBranchId_ShouldPass()
    {
        // Given
        var command = new DeleteBranchCommand(Guid.NewGuid());

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Delete Branch - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptyBranchId_ShouldFail()
    {
        // Given
        var command = new DeleteBranchCommand(Guid.Empty);

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.Id)
            .WithErrorMessage("Branch ID is required");
    }
}