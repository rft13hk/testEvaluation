using AutoMapper;
using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem;

/// <summary>
/// Profile for mapping between Application and API CreateSaleItem responses
/// </summary>
public class CreateSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSaleItem feature
    /// </summary>
    public CreateSaleItemProfile()
    {
        CreateMap<CreateSaleItemRequest, CreateSaleItemCommand>();
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
    }
}
