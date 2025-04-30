using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;

/// <summary>
/// Command for retrieving a SaleItem by their ID
/// </summary>
public record GetSaleItemCommand : IRequest<GetSaleItemResult>
{
    /// <summary>
    /// The unique identifier of the SaleItem to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetSaleItemCommand
    /// </summary>
    /// <param name="id">The ID of the SaleItem to retrieve</param>
    public GetSaleItemCommand(Guid id)
    {
        Id = id;
    }
}
