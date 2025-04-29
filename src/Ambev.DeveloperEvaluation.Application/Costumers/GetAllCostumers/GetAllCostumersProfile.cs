using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;

/// <summary>
/// Profile for mapping between Costumer entity and GetUserResponse
/// </summary>
public class GetAllCostumersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetAllCostumersProfile()
    {
        CreateMap<Costumer, GetAllCostumersResult.CostumerDto>();
        //CreateMap<Costumer, GetCostumerResult>();
    }
}
