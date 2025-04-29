using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Command for retrieving a user by their ID
/// </summary>
public record GetUserByEmailCommand : IRequest<GetUserResult>
{
    /// <summary>
    /// The unique identifier of the user to retrieve
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// Initializes a new instance of GetUserByEmailCommand
    /// </summary>
    /// <param name="id">The ID of the user to retrieve</param>
    public GetUserByEmailCommand(string email)
    {
        Email = email;
        
    }
}
