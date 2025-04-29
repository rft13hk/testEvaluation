using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetAllCostumers;

/// <summary>
/// Profile for mapping between Application and API CreateCostumer responses
/// </summary>
public class GetAllCostumersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCostumer feature
    /// </summary>
    public GetAllCostumersProfile()
    {
        CreateMap<GetAllCostumersResponse, GetAllCostumersResult>(); 
        CreateMap<GetAllCostumersRequest, GetAllCostumersCommand>();
        CreateMap<GetAllCostumersResult, GetAllCostumersResponse>();
        CreateMap<GetAllCostumersResult.CostumerDto, GetAllCostumersResponse.CostumerDto>();
    }
}
