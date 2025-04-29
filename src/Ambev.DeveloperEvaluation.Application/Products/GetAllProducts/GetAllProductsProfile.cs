using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Profile for mapping between Product entity and GetUserResponse
/// </summary>
public class GetAllProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetAllProductsProfile()
    {
        CreateMap<Product, GetAllProductsResult.ProductDto>();
        //CreateMap<Product, GetProductResult>();
    }
}
