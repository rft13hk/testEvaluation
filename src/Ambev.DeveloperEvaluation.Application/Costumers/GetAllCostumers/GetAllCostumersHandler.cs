using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;

/// <summary>
/// Handler for processing GetCostumerCommand requests
/// </summary>
public class GetAllCostumersHandler : IRequestHandler<GetAllCostumersCommand, GetAllCostumersResult>
{
    private readonly ICostumerRepository _CostumerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllCostumersHandler
    /// </summary>
    /// <param name="CostumerRepository">The Costumer repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCostumerCommand</param>
    public GetAllCostumersHandler(
        ICostumerRepository CostumerRepository,
        IMapper mapper)
    {
        _CostumerRepository = CostumerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllCostumersCommand request (Get paginated list)
    /// </summary>
    /// <param name="request">The GetAllCostumers command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of Costumer details</returns>
    public async Task<GetAllCostumersResult> Handle(GetAllCostumersCommand request, CancellationToken cancellationToken)
    {
        var Costumers = await _CostumerRepository.GetAllAsync(request.Page, request.Size, request.Order, request.ActiveRecordsOnly, cancellationToken);
        var totalCostumers = await _CostumerRepository.GetTotalCostumersCountAsync(request.ActiveRecordsOnly, cancellationToken);
        
        if (Costumers == null || !Costumers.Any())
        {
            return new GetAllCostumersResult
            {
                TotalItems = 0,
                TotalPages = 0,
                CurrentPage = request.Page,
                Costumers = Enumerable.Empty<GetAllCostumersResult.CostumerDto>()
            };
        }
        // Calculate total pages based on the total number of Costumers and the page size
        var totalPages = (int)Math.Ceiling((double)totalCostumers / request.Size);

        var CostumerResults = _mapper.Map<IEnumerable<GetAllCostumersResult.CostumerDto>>(Costumers);

        return new GetAllCostumersResult
        {
            TotalItems = totalCostumers,
            TotalPages = totalPages,
            CurrentPage = request.Page,
            Costumers = CostumerResults
        };
    }

}
