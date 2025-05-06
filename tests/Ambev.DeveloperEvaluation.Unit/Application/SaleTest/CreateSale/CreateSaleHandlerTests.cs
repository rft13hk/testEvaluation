using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.CreateSale;

/// <summary>
/// Unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
    /// </summary>
    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleHandler(_saleRepository, _userRepository, _mapper);
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given valid command When handling Then creates sale successfully")]
    public async Task Handle_ValidCommand_CreatesSaleSuccessfully()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = "Valid Sale",
            UserId = Guid.NewGuid()
        };

        var costumerId = Guid.NewGuid();
        var BrancheId = Guid.NewGuid();

        var sale = new Ambev.DeveloperEvaluation.Domain.Entities.Sale
        {
            Id = Guid.NewGuid(),
            SaleNumber = command.SaleNumber,
            UserId = command.UserId,
            User = new User { Id = command.UserId },
            CostumerId = costumerId,
            Costumer = new DeveloperEvaluation.Domain.Entities.Costumer()
            {
                Id = costumerId,
                UserId = command.UserId,
                Users = new User()
                {
                    Id = command.UserId
                }
            },
            BranchId = BrancheId,
            Branch = new DeveloperEvaluation.Domain.Entities.Branch()
            {
                Id = BrancheId,
                UserId = command.UserId,
                User = new User()
                {
                    Id = command.UserId
                }
            },
            CreateAt = DateTimeOffset.UtcNow,
            UpdateAt = DateTimeOffset.UtcNow
        };

        var result = new CreateSaleResult
        {
            Id = sale.Id
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns(new User { Id = command.UserId });
        _mapper.Map<Ambev.DeveloperEvaluation.Domain.Entities.Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(sale, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(sale, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand(); // Invalid command (empty fields)

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Create Sale - Given non-existent user When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentUser_ThrowsInvalidOperationException()
    {
        // Given
        var command = new CreateSaleCommand
        {
            SaleNumber = "Valid Sale",
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