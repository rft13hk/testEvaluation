using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler for processing GetSaleCommand requests
/// </summary>
public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
{
    private readonly ISaleRepository _SaleRepository;

    private readonly ISaleItemRepository _SaleItemRepository;

    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetSaleHandler
    /// </summary>
    /// <param name="SaleRepository">The Sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSaleCommand</param>
    public GetSaleHandler(
        ISaleRepository SaleRepository,
        ISaleItemRepository SaleItemRepository,
        IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _SaleItemRepository = SaleItemRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSaleCommand request
    /// </summary>
    /// <param name="request">The GetSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale details if found</returns>
    public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var Sale = await _SaleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (Sale == null)
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");

        var returnedSale = _mapper.Map<GetSaleResult>(Sale);

        var items = await _SaleItemRepository.GetActiveItemsBySaleIdAsync(request.Id, cancellationToken);
        if (items != null && items.Any())
        {
            var discounts = items.Sum(i => i.Discount);
            var values = items.Sum(i => i.TotalPrice);

            returnedSale.TotalDiscount = discounts;
            returnedSale.TotalValue = values;
            returnedSale.TotalWithDiscount = values - discounts;
            returnedSale.TotalItems = items.Count();
        }
        else
        {
            returnedSale.TotalDiscount = 0;
            returnedSale.TotalValue = 0;
            returnedSale.TotalWithDiscount = 0;
            returnedSale.TotalItems = 0;
        }

        return returnedSale;
    }
}
