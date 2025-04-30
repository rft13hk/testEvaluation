using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

/// <summary>
/// Handler for processing CreateSaleItemCommand requests.
/// </summary>
public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
{
    private readonly ISaleItemRepository _SaleItemRepository;
    private readonly IUserRepository _userRepository; // Assuming you need to validate the user
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleItemHandler.
    /// </summary>
    /// <param name="SaleItemRepository">The SaleItem repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateSaleItemHandler(ISaleItemRepository SaleItemRepository, IUserRepository userRepository, IMapper mapper)
    {
        _SaleItemRepository = SaleItemRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateSaleItemCommand request.
    /// </summary>
    /// <param name="command">The CreateSaleItem command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created SaleItem details.</returns>
    public async Task<CreateSaleItemResult> Handle(CreateSaleItemCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleItemValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Validate if the user creating the SaleItem exists
        var creatingUser = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (creatingUser == null)
            throw new InvalidOperationException($"User with ID {command.UserId} not found.");

        var SaleItem = _mapper.Map<SaleItem>(command);
        SaleItem.UserId = command.UserId; // Ensure UserId is set
        SaleItem.CreateAt = DateTimeOffset.UtcNow;
        SaleItem.UpdateAt = DateTimeOffset.UtcNow;

        

        var createdSaleItem = await _SaleItemRepository.CreateAsync(SaleItem, cancellationToken);
        var result = _mapper.Map<CreateSaleItemResult>(createdSaleItem);
        return result;
    }
}