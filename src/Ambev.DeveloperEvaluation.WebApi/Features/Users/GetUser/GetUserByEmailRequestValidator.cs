using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Validator for GetUserRequest
/// </summary>
public class GetUserByEmailRequestValidator : AbstractValidator<GetUserByEmailRequest>
{
    /// <summary>
    /// Initializes validation rules for GetUserRequest
    /// </summary>
    public GetUserByEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required");
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email format");
        
    }
}
