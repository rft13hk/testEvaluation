using AutoMapper;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem;

/// <summary>
/// Profile for mapping between Application and API CreateSaleItem responses
/// </summary>
public class GetSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSaleItem feature
    /// </summary>
    public GetSaleItemProfile()
    {
        CreateMap<GetSaleItemResult, GetSaleItemResponse>(); 
        CreateMap<GetSaleItemRequest, GetSaleItemCommand>();   
    }
}
