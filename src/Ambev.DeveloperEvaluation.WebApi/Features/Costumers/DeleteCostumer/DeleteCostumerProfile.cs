using Ambev.DeveloperEvaluation.Application.Costumers.DeleteCostumer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.DeleteCostumer;

/// <summary>
/// Profile for mapping DeleteCostumer feature requests to commands
/// </summary>
public class DeleteCostumerProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteCostumer feature
    /// </summary>
    public DeleteCostumerProfile()
    {
        CreateMap<Guid, Application.Costumers.DeleteCostumer.DeleteCostumerCommand>()
            .ConstructUsing(id => new Application.Costumers.DeleteCostumer.DeleteCostumerCommand(id));

        CreateMap<DeleteCostumerRequest, DeleteCostumerCommand>();   

    }
}
