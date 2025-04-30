using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Handler for processing GetSaleCommand requests
/// </summary>
public class GetAllSalesHandler : IRequestHandler<GetAllSalesCommand, GetAllSalesResult>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllSalesHandler
    /// </summary>
    /// <param name="SaleRepository">The Sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSaleCommand</param>
    public GetAllSalesHandler(
        ISaleRepository SaleRepository,
        IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllSalesCommand request (Get paginated list)
    /// </summary>
    /// <param name="request">The GetAllSales command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of Sale details</returns>
    public async Task<GetAllSalesResult> Handle(GetAllSalesCommand request, CancellationToken cancellationToken)
    {
        var Sales = await _SaleRepository.GetAllAsync(request.Page, request.Size, request.Order, request.ActiveRecordsOnly, cancellationToken);
        var totalSales = await _SaleRepository.GetTotalSalesCountAsync(request.ActiveRecordsOnly, cancellationToken);
        
        if (Sales == null || !Sales.Any())
        {
            return new GetAllSalesResult
            {
                TotalItems = 0,
                TotalPages = 0,
                CurrentPage = request.Page,
                Sales = Enumerable.Empty<GetAllSalesResult.SaleDto>()
            };
        }
        // Calculate total pages based on the total number of Sales and the page size
        var totalPages = (int)Math.Ceiling((double)totalSales / request.Size);

        var SaleResults = _mapper.Map<IEnumerable<GetAllSalesResult.SaleDto>>(Sales);

        return new GetAllSalesResult
        {
            TotalItems = totalSales,
            TotalPages = totalPages,
            CurrentPage = request.Page,
            Sales = SaleResults
        };
    }

}
