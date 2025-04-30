using AutoMapper;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetAllSaleItems;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetAllSaleItems;

/// <summary>
/// Profile for mapping between Application and API CreateSaleItem responses
/// </summary>
public class GetAllSaleItemsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSaleItem feature
    /// </summary>
    public GetAllSaleItemsProfile()
    {
        CreateMap<GetAllSaleItemsResponse, GetAllSaleItemsResult>(); 
        CreateMap<GetAllSaleItemsRequest, GetAllSaleItemsCommand>();
        CreateMap<GetAllSaleItemsResult, GetAllSaleItemsResponse>();
        CreateMap<GetAllSaleItemsResult.SaleItemDto, GetAllSaleItemsResponse.SaleItemDto>();
    }
}
