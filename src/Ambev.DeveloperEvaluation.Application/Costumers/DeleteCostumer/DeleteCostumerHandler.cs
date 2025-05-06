using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Costumers.DeleteCostumer;

/// <summary>
/// Handler for processing DeleteCostumerCommand requests
/// </summary>
public class DeleteCostumerHandler : IRequestHandler<DeleteCostumerCommand, DeleteCostumerResponse>
{
    private readonly ICostumerRepository _CostumerRepository;

    /// <summary>
    /// Initializes a new instance of DeleteUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="validator">The validator for DeleteUserCommand</param>
    public DeleteCostumerHandler(
        ICostumerRepository CostumerRepository)
    {
        _CostumerRepository = CostumerRepository;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request
    /// </summary>
    /// <param name="request">The DeleteUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteCostumerResponse> Handle(DeleteCostumerCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCostumerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _CostumerRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Costumer with ID {request.Id} not found.");

        return new DeleteCostumerResponse { Success = true };
    }
}
