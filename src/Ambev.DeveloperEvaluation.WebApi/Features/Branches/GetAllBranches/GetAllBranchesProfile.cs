using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetAllBranches;

/// <summary>
/// Profile for mapping between Application and API CreateBranch responses
/// </summary>
public class GetAllBranchesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateBranch feature
    /// </summary>
    public GetAllBranchesProfile()
    {
        CreateMap<GetAllBranchesResponse, GetAllBranchesResult>(); 
        CreateMap<GetAllBranchesRequest, GetAllBranchesCommand>();
        CreateMap<GetAllBranchesResult, GetAllBranchesResponse>();
        CreateMap<GetAllBranchesResult.BranchDto, GetAllBranchesResponse.BranchDto>();
    }
}
