using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;

/// <summary>
/// Handler for processing CreateCostumerCommand requests.
/// </summary>
public class CreateCostumerHandler : IRequestHandler<CreateCostumerCommand, CreateCostumerResult>
{
    private readonly ICostumerRepository _CostumerRepository;
    private readonly IUserRepository _userRepository; // Assuming you need to validate the user
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateCostumerHandler.
    /// </summary>
    /// <param name="CostumerRepository">The Costumer repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateCostumerHandler(ICostumerRepository CostumerRepository, IUserRepository userRepository, IMapper mapper)
    {
        _CostumerRepository = CostumerRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateCostumerCommand request.
    /// </summary>
    /// <param name="command">The CreateCostumer command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created Costumer details.</returns>
    public async Task<CreateCostumerResult> Handle(CreateCostumerCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCostumerValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Validate if the user creating the Costumer exists
        var creatingUser = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (creatingUser == null)
            throw new InvalidOperationException($"User with ID {command.UserId} not found.");

        var Costumer = _mapper.Map<Costumer>(command);
        Costumer.UserId = command.UserId; // Ensure UserId is set
        Costumer.CreateAt = DateTimeOffset.UtcNow;
        Costumer.UpdateAt = DateTimeOffset.UtcNow;

        var createdCostumer = await _CostumerRepository.CreateAsync(Costumer, cancellationToken);
        var result = _mapper.Map<CreateCostumerResult>(createdCostumer);
        return result;
    }
}