using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;

/// <summary>
/// Profile for mapping between Application and API CreateBranch responses
/// </summary>
public class CreateBranchProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateBranch feature
    /// </summary>
    public CreateBranchProfile()
    {
        CreateMap<CreateBranchRequest, CreateBranchCommand>();
        CreateMap<CreateBranchResult, CreateBranchResponse>();
    }
}
