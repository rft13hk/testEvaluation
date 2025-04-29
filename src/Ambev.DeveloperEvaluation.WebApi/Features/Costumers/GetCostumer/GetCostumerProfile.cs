using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetCostumer;

/// <summary>
/// Profile for mapping between Application and API CreateCostumer responses
/// </summary>
public class GetCostumerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCostumer feature
    /// </summary>
    public GetCostumerProfile()
    {
        CreateMap<GetCostumerResult, GetCostumerResponse>(); 
        CreateMap<GetCostumerRequest, GetCostumerCommand>();   
    }
}
