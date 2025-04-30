using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

/// <summary>
/// Profile for mapping between SaleItem entity and CreateSaleItemResponse.
/// </summary>
public class CreateSaleItemProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSaleItem operation.
    /// </summary>
    public CreateSaleItemProfile()
    {
        CreateMap<CreateSaleItemCommand, SaleItem>();
        CreateMap<SaleItem, CreateSaleItemResult>();
    }
}