using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.CreateCostumer;

/// <summary>
/// Profile for mapping between Application and API CreateCostumer responses
/// </summary>
public class CreateCostumerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCostumer feature
    /// </summary>
    public CreateCostumerProfile()
    {
        CreateMap<CreateCostumerRequest, CreateCostumerCommand>();
        CreateMap<CreateCostumerResult, CreateCostumerResponse>();
    }
}
