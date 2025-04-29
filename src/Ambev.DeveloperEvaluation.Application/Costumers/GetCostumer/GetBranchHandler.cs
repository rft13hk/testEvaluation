using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;

/// <summary>
/// Handler for processing GetCostumerCommand requests
/// </summary>
public class GetCostumerHandler : IRequestHandler<GetCostumerCommand, GetCostumerResult>
{
    private readonly ICostumerRepository _CostumerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCostumerHandler
    /// </summary>
    /// <param name="CostumerRepository">The Costumer repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCostumerCommand</param>
    public GetCostumerHandler(
        ICostumerRepository CostumerRepository,
        IMapper mapper)
    {
        _CostumerRepository = CostumerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCostumerCommand request
    /// </summary>
    /// <param name="request">The GetCostumer command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Costumer details if found</returns>
    public async Task<GetCostumerResult> Handle(GetCostumerCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCostumerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var Costumer = await _CostumerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (Costumer == null)
            throw new KeyNotFoundException($"Costumer with ID {request.Id} not found");

        return _mapper.Map<GetCostumerResult>(Costumer);
    }
}
