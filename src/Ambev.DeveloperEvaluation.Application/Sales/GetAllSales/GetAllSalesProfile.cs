using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Profile for mapping between Sale entity and GetUserResponse
/// </summary>
public class GetAllSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetAllSalesProfile()
    {
        CreateMap<Sale, GetAllSalesResult.SaleDto>();
        //CreateMap<Sale, GetSaleResult>();
    }
}
