using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;

/// <summary>
/// Profile for mapping between SaleItem entity and GetUserResponse
/// </summary>
public class GetSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetSaleItemProfile()
    {
        CreateMap<GetSaleItemCommand, SaleItem>();
        CreateMap<SaleItem, GetSaleItemResult>();
    }
}
