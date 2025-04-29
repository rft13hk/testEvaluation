using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Handler for processing GetProductCommand requests
/// </summary>
public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, GetAllProductsResult>
{
    private readonly IProductRepository _ProductRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllProductsHandler
    /// </summary>
    /// <param name="ProductRepository">The Product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetProductCommand</param>
    public GetAllProductsHandler(
        IProductRepository ProductRepository,
        IMapper mapper)
    {
        _ProductRepository = ProductRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllProductsCommand request (Get paginated list)
    /// </summary>
    /// <param name="request">The GetAllProducts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of Product details</returns>
    public async Task<GetAllProductsResult> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
    {
        var Products = await _ProductRepository.GetAllAsync(request.Page, request.Size, request.Order, request.ActiveRecordsOnly, cancellationToken);
        var totalProducts = await _ProductRepository.GetTotalProductsCountAsync(request.ActiveRecordsOnly, cancellationToken);
        
        if (Products == null || !Products.Any())
        {
            return new GetAllProductsResult
            {
                TotalItems = 0,
                TotalPages = 0,
                CurrentPage = request.Page,
                Products = Enumerable.Empty<GetAllProductsResult.ProductDto>()
            };
        }
        // Calculate total pages based on the total number of Products and the page size
        var totalPages = (int)Math.Ceiling((double)totalProducts / request.Size);

        var ProductResults = _mapper.Map<IEnumerable<GetAllProductsResult.ProductDto>>(Products);

        return new GetAllProductsResult
        {
            TotalItems = totalProducts,
            TotalPages = totalPages,
            CurrentPage = request.Page,
            Products = ProductResults
        };
    }

}
