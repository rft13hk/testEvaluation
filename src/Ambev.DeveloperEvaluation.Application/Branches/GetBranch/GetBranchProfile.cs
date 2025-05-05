using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

/// <summary>
/// Profile for mapping between Branch entity and GetUserResponse
/// </summary>
public class GetBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetBranchProfile()
    {
        CreateMap<GetBranchCommand, Branch>();
        CreateMap<Branch, GetBranchResult>();
        CreateMap<User, GetBranchResultUser>();
    }
}
