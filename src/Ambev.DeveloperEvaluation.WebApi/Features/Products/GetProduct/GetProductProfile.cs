using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Profile for mapping between Application and API CreateProduct responses
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct feature
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<GetProductResult, GetProductResponse>(); 
        CreateMap<GetProductRequest, GetProductCommand>();   
    }
}
