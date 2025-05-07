using System.Data.Common;
using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.Shared;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.CreateSaleItem;

/// <summary>
/// Unit tests for the <see cref="CreateSaleItemHandler"/> class.
/// </summary>
public class CreateSaleItemHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IProductRepository _productRepository; // Assuming you need to validate the product
    private readonly IUserRepository _userRepository; // Assuming you need to validate the user

    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    private readonly CreateSaleItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleItemHandlerTests"/> class.
    /// </summary>
    public CreateSaleItemHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _branchRepository = Substitute.For<IBranchRepository>();

        _handler = new CreateSaleItemHandler(_saleRepository, _saleItemRepository, _userRepository, _productRepository, _mapper);
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given valid command When handling Then creates saleItem successfully")]
    public async Task Handle_ValidCommand_CreatesSaleItemSuccessfully()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Quantity = 10
        };

        var _saleItemsScenariosTest = new SaleItemsScenariosTest();

        var fackeUser = await _saleItemsScenariosTest.CreateFackeUser();

        var fackeCostumer = await _saleItemsScenariosTest.CreateFackeCostumer(fackeUser);

        var fackeBranche = await _saleItemsScenariosTest.CreateFackeBranch(fackeUser);

        var fackeSale = await _saleItemsScenariosTest.CreateFackeSale(fackeCostumer,fackeBranche, fackeUser);

        var fackeProduct = await _saleItemsScenariosTest.CreateFackeProduct(fackeUser);

        var fackeSaleItem = await _saleItemsScenariosTest.CreateFackeSaleItem(fackeSale, fackeProduct, fackeUser);

        var result = new CreateSaleItemResult
        {
            Id = fackeSaleItem.Id
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns(new User { Id = command.UserId });
        _mapper.Map<Ambev.DeveloperEvaluation.Domain.Entities.SaleItem>(command).Returns(fackeSaleItem);
        _saleItemRepository.CreateAsync(fackeSaleItem, Arg.Any<CancellationToken>()).Returns(fackeSaleItem);
        _mapper.Map<CreateSaleItemResult>(fackeSaleItem).Returns(result);
        _productRepository.GetByIdAsync(command.ProductId, Arg.Any<CancellationToken>())
            .Returns(new Ambev.DeveloperEvaluation.Domain.Entities.Product 
            { 
                Id = fackeUser.Id, 
                User = fackeUser
            });

        // When
        var createSaleItemResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleItemResult.Should().NotBeNull();
        createSaleItemResult.Id.Should().Be(fackeSaleItem.Id);
        await _saleItemRepository.Received(1).CreateAsync(fackeSaleItem, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleItemCommand(); // Invalid command (empty fields)

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Create SaleItem - Given non-existent user When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentUser_ThrowsInvalidOperationException()
    {
        // Given
        var command = new CreateSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Quantity = 10,
            UserId = Guid.NewGuid()   
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns((User)null!);

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"User with ID {command.UserId} not found.");
    }
}