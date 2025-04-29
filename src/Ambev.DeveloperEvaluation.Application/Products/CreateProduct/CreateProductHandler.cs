using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests.
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _ProductRepository;
    private readonly IUserRepository _userRepository; // Assuming you need to validate the user
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler.
    /// </summary>
    /// <param name="ProductRepository">The Product repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateProductHandler(IProductRepository ProductRepository, IUserRepository userRepository, IMapper mapper)
    {
        _ProductRepository = ProductRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateProductCommand request.
    /// </summary>
    /// <param name="command">The CreateProduct command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created Product details.</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Validate if the user creating the Product exists
        var creatingUser = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (creatingUser == null)
            throw new InvalidOperationException($"User with ID {command.UserId} not found.");

        var Product = _mapper.Map<Product>(command);
        Product.UserId = command.UserId; // Ensure UserId is set
        Product.CreateAt = DateTimeOffset.UtcNow;
        Product.UpdateAt = DateTimeOffset.UtcNow;

        var createdProduct = await _ProductRepository.CreateAsync(Product, cancellationToken);
        var result = _mapper.Map<CreateProductResult>(createdProduct);
        return result;
    }
}