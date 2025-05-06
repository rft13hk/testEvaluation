using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.CreateBranch;

/// <summary>
/// Unit tests for the <see cref="CreateBranchCommand"/> class.
/// </summary>
public class CreateBranchCommandTests
{
    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given valid data When validating Then returns valid result")]
    public void Validate_ValidData_ShouldReturnValidResult()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = "Valid Branch Name",
            UserId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().BeEmpty();
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given empty branch name When validating Then returns invalid result")]
    public void Validate_EmptyBranchName_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = string.Empty,
            UserId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then

        validationResult.IsValid.Should().BeFalse();

        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "Branch name is required.");
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given empty user ID When validating Then returns invalid result")]
    public void Validate_EmptyUserId_ShouldReturnInvalidResult()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = "Valid Branch Name",
            UserId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle(error =>
            error.Detail == "User ID is required.");
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Create Branch - Given empty branch name and user ID When validating Then returns multiple errors")]
    public void Validate_EmptyBranchNameAndUserId_ShouldReturnMultipleErrors()
    {
        // Given
        var command = new CreateBranchCommand
        {
            BranchName = string.Empty,
            UserId = Guid.Empty
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(2);
        validationResult.Errors.Should().Contain(error => error.Detail == "Branch name is required.");
        validationResult.Errors.Should().Contain(error => error.Detail == "User ID is required.");
    }
}
