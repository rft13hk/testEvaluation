using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;

/// <summary>
/// Handler for processing GetSaleItemCommand requests
/// </summary>
public class GetSaleItemHandler : IRequestHandler<GetSaleItemCommand, GetSaleItemResult>
{
    private readonly ISaleItemRepository _SaleItemRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetSaleItemHandler
    /// </summary>
    /// <param name="SaleItemRepository">The SaleItem repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetSaleItemCommand</param>
    public GetSaleItemHandler(
        ISaleItemRepository SaleItemRepository,
        IMapper mapper)
    {
        _SaleItemRepository = SaleItemRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSaleItemCommand request
    /// </summary>
    /// <param name="request">The GetSaleItem command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The SaleItem details if found</returns>
    public async Task<GetSaleItemResult> Handle(GetSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleItemValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var SaleItem = await _SaleItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (SaleItem == null)
            throw new KeyNotFoundException($"SaleItem with ID {request.Id} not found");

        return _mapper.Map<GetSaleItemResult>(SaleItem);
    }
}
