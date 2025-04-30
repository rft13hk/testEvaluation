using MediatR;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem;

/// <summary>
/// Command for deleting a SaleItem
/// </summary>
public record DeleteSaleItemCommand : IRequest<DeleteSaleItemResponse>
{
    /// <summary>
    /// The unique identifier of the SaleItem to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteSaleItemCommand
    /// </summary>
    /// <param name="id">The ID of the SaleItem to delete</param>
    public DeleteSaleItemCommand(Guid id)
    {
        Id = id;
    }
}
