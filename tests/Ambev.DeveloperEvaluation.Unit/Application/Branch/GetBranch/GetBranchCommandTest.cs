using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.GetBranch;

/// <summary>
/// Unit tests for the <see cref="GetBranchCommand"/> class.
/// </summary>
public class GetBranchCommandTest
{
    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Get Branch - Given valid branch ID When validating Then validation succeeds")]
    public void Validate_ValidBranchId_ShouldPass()
    {
        // Given
        var command = new GetBranchCommand(Guid.NewGuid());
        var validator = new GetBranchValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Get Branch - Given empty branch ID When validating Then validation fails")]
    public void Validate_EmptyBranchId_ShouldFail()
    {
        // Given
        var command = new GetBranchCommand(Guid.Empty);
        var validator = new GetBranchValidator();

        // When
        var validationResult = validator.Validate(command);

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.PropertyName == nameof(GetBranchCommand.Id) &&
            error.ErrorMessage == "Branch ID is required");
    }
}