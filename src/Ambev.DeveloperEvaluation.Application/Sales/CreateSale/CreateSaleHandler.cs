using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _SaleRepository;
    private readonly IUserRepository _userRepository; // Assuming you need to validate the user
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler.
    /// </summary>
    /// <param name="SaleRepository">The Sale repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateSaleHandler(ISaleRepository SaleRepository, IUserRepository userRepository, IMapper mapper)
    {
        _SaleRepository = SaleRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request.
    /// </summary>
    /// <param name="command">The CreateSale command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created Sale details.</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Validate if the user creating the Sale exists
        var creatingUser = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (creatingUser == null)
            throw new InvalidOperationException($"User with ID {command.UserId} not found.");

        var Sale = _mapper.Map<Sale>(command);
        Sale.UserId = command.UserId; // Ensure UserId is set
        Sale.CreateAt = DateTimeOffset.UtcNow;
        Sale.UpdateAt = DateTimeOffset.UtcNow;

        var createdSale = await _SaleRepository.CreateAsync(Sale, cancellationToken);
        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}