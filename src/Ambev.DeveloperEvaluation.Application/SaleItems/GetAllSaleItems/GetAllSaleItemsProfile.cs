using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetAllSaleItems;

/// <summary>
/// Profile for mapping between SaleItem entity and GetUserResponse
/// </summary>
public class GetAllSaleItemsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetAllSaleItemsProfile()
    {
        CreateMap<SaleItem, GetAllSaleItemsResult.SaleItemDto>();
        //CreateMap<SaleItem, GetSaleItemResult>();
    }
}
