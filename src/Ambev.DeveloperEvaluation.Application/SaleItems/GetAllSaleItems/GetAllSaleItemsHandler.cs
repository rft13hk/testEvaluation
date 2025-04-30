using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetAllSaleItems;

/// <summary>
/// Handler for processing GetSaleItemCommand requests
/// </summary>
public class GetAllSaleItemsHandler : IRequestHandler<GetAllSaleItemsCommand, GetAllSaleItemsResult>
{
    private readonly ISaleItemRepository _SaleItemRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllSaleItemsHandler
    /// </summary>
    /// <param name="SaleItemRepository">The SaleItem repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSaleItemCommand</param>
    public GetAllSaleItemsHandler(
        ISaleItemRepository SaleItemRepository,
        IMapper mapper)
    {
        _SaleItemRepository = SaleItemRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllSaleItemsCommand request (Get paginated list)
    /// </summary>
    /// <param name="request">The GetAllSaleItems command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of SaleItem details</returns>
    public async Task<GetAllSaleItemsResult> Handle(GetAllSaleItemsCommand request, CancellationToken cancellationToken)
    {
        var SaleItems = await _SaleItemRepository.GetAllAsync(request.Page, request.Size, request.Order, request.ActiveRecordsOnly, cancellationToken);
        var totalSaleItems = await _SaleItemRepository.GetTotalSaleItemsCountAsync(request.ActiveRecordsOnly, cancellationToken);
        
        if (SaleItems == null || !SaleItems.Any())
        {
            return new GetAllSaleItemsResult
            {
                TotalItems = 0,
                TotalPages = 0,
                CurrentPage = request.Page,
                SaleItems = Enumerable.Empty<GetAllSaleItemsResult.SaleItemDto>()
            };
        }
        // Calculate total pages based on the total number of SaleItems and the page size
        var totalPages = (int)Math.Ceiling((double)totalSaleItems / request.Size);

        var SaleItemResults = _mapper.Map<IEnumerable<GetAllSaleItemsResult.SaleItemDto>>(SaleItems);

        return new GetAllSaleItemsResult
        {
            TotalItems = totalSaleItems,
            TotalPages = totalPages,
            CurrentPage = request.Page,
            SaleItems = SaleItemResults
        };
    }

}
