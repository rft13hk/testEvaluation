using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.CreateBranch;

/// <summary>
/// Unit tests for the <see cref="CreateBranchValidator"/> class.
/// </summary>
public class CreateBranchValidatorTests
{
    private readonly CreateBranchValidator _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBranchValidatorTests"/> class.
    /// </summary>
    public CreateBranchValidatorTests()
    {
        _validator = new CreateBranchValidator();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given valid data When validating Then validation succeeds")]
    public void Validate_ValidData_ShouldPass()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = "Valid Branch Name",
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given empty branch name When validating Then validation fails")]
    public void Validate_EmptyBranchName_ShouldFail()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = string.Empty,
            UserId = Guid.NewGuid()
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.BranchName)
            .WithErrorMessage("Branch name is required.");
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given empty user ID When validating Then validation fails")]
    public void Validate_EmptyUserId_ShouldFail()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = "Valid Branch Name",
            UserId = Guid.Empty
        };

        // When
        var result = _validator.TestValidate(command);

        // Then
        result.ShouldHaveValidationErrorFor(c => c.UserId)
            .WithErrorMessage("User ID is required.");
    }
}