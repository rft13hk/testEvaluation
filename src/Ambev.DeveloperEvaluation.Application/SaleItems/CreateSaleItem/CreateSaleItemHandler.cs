using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;

/// <summary>
/// Handler for processing CreateSaleItemCommand requests.
/// </summary>
public class CreateSaleItemHandler : IRequestHandler<CreateSaleItemCommand, CreateSaleItemResult>
{
    private readonly ISaleItemRepository _SaleItemRepository;

    private readonly IProductRepository _productRepository; // Assuming you need to validate the product
    private readonly IUserRepository _userRepository; // Assuming you need to validate the user
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateSaleItemHandler.
    /// </summary>
    /// <param name="SaleItemRepository">The SaleItem repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateSaleItemHandler(ISaleItemRepository SaleItemRepository, IUserRepository userRepository, IProductRepository productRepository, IMapper mapper)
    {
        _SaleItemRepository = SaleItemRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
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

        if (SaleItem.Quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (SaleItem.Quantity >20)
            throw new ArgumentException("Quantity must be less than or equal to 20.");

        var priceProduct = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken);
        if (priceProduct == null)
            throw new InvalidOperationException($"Product with ID {command.ProductId} not found.");

        SaleItem.Price = priceProduct.Price;

        SaleItem.Discount = CalculateDiscount(SaleItem.Quantity, SaleItem.Price);

        SaleItem.TotalPrice = SaleItem.Quantity * SaleItem.Price;

        SaleItem.StatusItem = SaleItemStatus.NotCancelled;
        

        var createdSaleItem = await _SaleItemRepository.CreateAsync(SaleItem, cancellationToken);
        var result = _mapper.Map<CreateSaleItemResult>(createdSaleItem);
        return result;
    }

    private decimal CalculateDiscount(int quantity, decimal price)
    {
        var value = quantity * price;

        if (quantity >= 4 && quantity < 10)
        {
            return  value * 0.10m; // 10% discount
        }
        else if (quantity >= 10 && quantity <= 20)
        {
            return value * 0.20m; // 20% discount
        }

        return 0.0m; // No discount
    }


}