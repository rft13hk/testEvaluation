using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Validator for CreateBranchCommand that defines validation rules for branch creation command.
/// </summary>
public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateBranchCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - BranchName: Required, must not be null or empty.
    /// - UserId: Required, must not be an empty Guid.
    /// </remarks>
    public CreateBranchValidator()
    {
        RuleFor(branch => branch.BranchName)
            .NotEmpty()
            .WithMessage("Branch name is required.");

        RuleFor(branch => branch.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("User ID is required.");
    }
}