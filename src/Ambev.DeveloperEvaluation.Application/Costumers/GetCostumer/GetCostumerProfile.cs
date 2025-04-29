using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;

/// <summary>
/// Profile for mapping between Costumer entity and GetUserResponse
/// </summary>
public class GetCostumerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetCostumerProfile()
    {
        CreateMap<GetCostumerCommand, Costumer>();
        CreateMap<Costumer, GetCostumerResult>();
    }
}
