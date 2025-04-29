using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;

/// <summary>
/// Profile for mapping between Costumer entity and CreateCostumerResponse.
/// </summary>
public class CreateCostumerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCostumer operation.
    /// </summary>
    public CreateCostumerProfile()
    {
        CreateMap<CreateCostumerCommand, Costumer>();
        CreateMap<Costumer, CreateCostumerResult>();
    }
}