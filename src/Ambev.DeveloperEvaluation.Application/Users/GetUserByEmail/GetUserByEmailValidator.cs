using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Validator for GetUserByEmailCommand
/// </summary>
public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserByEmailCommand
    /// </summary>
    public GetUserByEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("User Email is required");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email format");
    }
}
