using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetAllBranches;

/// <summary>
/// Validator for GetBranchRequest
/// </summary>
public class GetAllBranchesRequestValidator : AbstractValidator<GetAllBranchesRequest>
{
    /// <summary>
    /// Initializes validation rules for GetBranchRequest
    /// </summary>
    public GetAllBranchesRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
}
