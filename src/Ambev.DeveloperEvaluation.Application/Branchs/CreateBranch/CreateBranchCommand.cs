using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Command for creating a new branch.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a branch,
/// including the branch name and the ID of the user who created it.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="CreateBranchResult"/>.
///
/// The data provided in this command is validated using the
/// <see cref="CreateBranchValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required rules.
/// </remarks>
public class CreateBranchCommand : IRequest<CreateBranchResult>
{
    /// <summary>
    /// Gets or sets the name of the branch to be created.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the user who created this branch.
    /// </summary>
    public Guid UserId { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateBranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}