using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem;

/// <summary>
/// Handler for processing DeleteSaleItemCommand requests
/// </summary>
public class DeleteSaleItemHandler : IRequestHandler<DeleteSaleItemCommand, DeleteSaleItemResponse>
{
    private readonly ISaleItemRepository _SaleItemRepository;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="validator">The validator for DeleteUserCommand</param>
    public DeleteSaleItemHandler(
        ISaleItemRepository SaleItemRepository)
    {
        _SaleItemRepository = SaleItemRepository;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="request">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteSaleItemResponse> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleItemValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _SaleItemRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"SaleItem with ID {request.Id} not found.");

        return new DeleteSaleItemResponse { Success = true };
    }
}
