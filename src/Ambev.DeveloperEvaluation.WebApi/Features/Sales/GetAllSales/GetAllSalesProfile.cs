using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class GetAllSalesProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public GetAllSalesProfile()
    {
        CreateMap<GetAllSalesResponse, GetAllSalesResult>(); 
        CreateMap<GetAllSalesRequest, GetAllSalesCommand>();
        CreateMap<GetAllSalesResult, GetAllSalesResponse>();
        CreateMap<GetAllSalesResult.SaleDto, GetAllSalesResponse.SaleDto>();
    }
}
