using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// Profile for mapping between Application and API CreateProduct responses
/// </summary>
public class GetAllProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct feature
    /// </summary>
    public GetAllProductsProfile()
    {
        CreateMap<GetAllProductsResponse, GetAllProductsResult>(); 
        CreateMap<GetAllProductsRequest, GetAllProductsCommand>();
        CreateMap<GetAllProductsResult, GetAllProductsResponse>();
        CreateMap<GetAllProductsResult.ProductDto, GetAllProductsResponse.ProductDto>();
    }
}
