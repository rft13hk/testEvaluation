namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Request model for getting a user by ID
/// </summary>
public class GetUserByEmailRequest
{
    /// <summary>
    /// The email of the user to retrieve
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
