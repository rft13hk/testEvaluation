using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.CreateProduct;

/// <summary>
/// Unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class CreateProductHandlerTests
{
    private readonly IProductRepository _costumerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductHandlerTests"/> class.
    /// </summary>
    public CreateProductHandlerTests()
    {
        _costumerRepository = Substitute.For<IProductRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(_costumerRepository, _userRepository, _mapper);
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given valid command When handling Then creates costumer successfully")]
    public async Task Handle_ValidCommand_CreatesProductSuccessfully()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = "Valid Product",
            UserId = Guid.NewGuid()
        };

        var costumer = new Ambev.DeveloperEvaluation.Domain.Entities.Product
        {
            Id = Guid.NewGuid(),
            ProductName = command.ProductName,
            UserId = command.UserId,
            User = new User { Id = command.UserId },
            CreateAt = DateTimeOffset.UtcNow,
            UpdateAt = DateTimeOffset.UtcNow
        };

        var result = new CreateProductResult
        {
            Id = costumer.Id
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns(new User { Id = command.UserId });
        _mapper.Map<Ambev.DeveloperEvaluation.Domain.Entities.Product>(command).Returns(costumer);
        _costumerRepository.CreateAsync(costumer, Arg.Any<CancellationToken>()).Returns(costumer);
        _mapper.Map<CreateProductResult>(costumer).Returns(result);

        // When
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(costumer.Id);
        await _costumerRepository.Received(1).CreateAsync(costumer, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Given
        var command = new CreateProductCommand(); // Invalid command (empty fields)

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Create Product - Given non-existent user When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentUser_ThrowsInvalidOperationException()
    {
        // Given
        var command = new CreateProductCommand
        {
            ProductName = "Valid Product",
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