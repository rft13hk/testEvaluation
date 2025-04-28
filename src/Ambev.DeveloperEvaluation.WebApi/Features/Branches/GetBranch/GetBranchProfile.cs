using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;

/// <summary>
/// Profile for mapping between Application and API CreateBranch responses
/// </summary>
public class GetBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateBranch feature
    /// </summary>
    public GetBranchProfile()
    {
        CreateMap<GetBranchResult, GetBranchResponse>(); 
        CreateMap<GetBranchRequest, GetBranchCommand>();   
    }
}
