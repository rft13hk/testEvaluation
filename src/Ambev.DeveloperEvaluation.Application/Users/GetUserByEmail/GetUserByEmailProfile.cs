using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserByEmailProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserByEmailProfile()
    {
        CreateMap<GetUserCommand, User>();
        CreateMap<GetUserByEmailCommand, User>();
        CreateMap<User, GetUserByEmailResult>();
    }
}
