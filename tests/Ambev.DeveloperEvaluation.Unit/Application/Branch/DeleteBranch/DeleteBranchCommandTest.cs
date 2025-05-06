using Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch.DeleteBranch;

/// <summary>
/// Unit tests for the <see cref="DeleteBranchCommand"/> class.
/// </summary>
public class DeleteBranchCommandTest
{
    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Delete Branch - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidBranchId_ShouldPass()
    {
        // Given
        var command = new DeleteBranchCommand(Guid.NewGuid());   

        var validator = new DeleteBranchValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Delete Branch - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptyBranchId_ShouldFail()
    {
        // Given
        var command = new DeleteBranchCommand(Guid.Empty);

        var validator = new DeleteBranchValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(DeleteBranchCommand.Id) &&
            error.ErrorMessage == "Branch ID is required");
    }
}