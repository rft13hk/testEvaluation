using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Handler for processing GetSaleCommand requests
/// </summary>
public class GetAllSalesHandler : IRequestHandler<GetAllSalesCommand, GetAllSalesResult>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly ISaleItemRepository _SaleItemRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllSalesHandler
    /// </summary>
    /// <param name="SaleRepository">The Sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSaleCommand</param>
    public GetAllSalesHandler(
        ISaleRepository SaleRepository,
        ISaleItemRepository SaleItemRepository,
        IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _SaleItemRepository = SaleItemRepository;
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

        foreach (var Sale in SaleResults)
        {
            var items = await _SaleItemRepository.GetActiveItemsBySaleIdAsync(Sale.Id, cancellationToken);
            if (items != null && items.Any())
            {
                var discounts = items.Sum(i => i.Discount);
                var values = items.Sum(i => i.TotalPrice);

                Sale.TotalDiscount = discounts;
                Sale.TotalValue = values;
                Sale.TotalWithDiscount = values - discounts;
                Sale.TotalItems = items.Count();
            }
            else
            {
                Sale.TotalDiscount = 0;
                Sale.TotalValue = 0;
                Sale.TotalWithDiscount = 0;
                Sale.TotalItems = 0;
            }
        }


        return new GetAllSalesResult
        {
            TotalItems = totalSales,
            TotalPages = totalPages,
            CurrentPage = request.Page,
            Sales = SaleResults
        };
    }

}
