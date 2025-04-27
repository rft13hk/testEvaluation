using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings;

public class AuthenticateUserRequest : Profile
{
    public AuthenticateUserRequest()
    {
        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>().ReverseMap();
    }
}