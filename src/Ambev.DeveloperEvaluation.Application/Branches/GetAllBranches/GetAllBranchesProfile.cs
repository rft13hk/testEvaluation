using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;

/// <summary>
/// Profile for mapping between Branch entity and GetUserResponse
/// </summary>
public class GetAllBranchesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetAllBranchesProfile()
    {
        CreateMap<Branch, GetAllBranchesResult.BranchDto>();
        //CreateMap<Branch, GetBranchResult>();
    }
}
