using Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.CreateCostumer;

/// <summary>
/// Unit tests for the <see cref="CreateCostumerHandler"/> class.
/// </summary>
public class CreateCostumerHandlerTests
{
    private readonly ICostumerRepository _costumerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly CreateCostumerHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCostumerHandlerTests"/> class.
    /// </summary>
    public CreateCostumerHandlerTests()
    {
        _costumerRepository = Substitute.For<ICostumerRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCostumerHandler(_costumerRepository, _userRepository, _mapper);
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given valid command When handling Then creates costumer successfully")]
    public async Task Handle_ValidCommand_CreatesCostumerSuccessfully()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = "Valid Costumer",
            UserId = Guid.NewGuid()
        };

        var costumer = new Ambev.DeveloperEvaluation.Domain.Entities.Costumer
        {
            Id = Guid.NewGuid(),
            CostumerName = command.CostumerName,
            UserId = command.UserId,
            Users = new User { Id = command.UserId },
            CreateAt = DateTimeOffset.UtcNow,
            UpdateAt = DateTimeOffset.UtcNow
        };

        var result = new CreateCostumerResult
        {
            Id = costumer.Id
        };

        _userRepository.GetByIdAsync(command.UserId, Arg.Any<CancellationToken>()).Returns(new User { Id = command.UserId });
        _mapper.Map<Ambev.DeveloperEvaluation.Domain.Entities.Costumer>(command).Returns(costumer);
        _costumerRepository.CreateAsync(costumer, Arg.Any<CancellationToken>()).Returns(costumer);
        _mapper.Map<CreateCostumerResult>(costumer).Returns(result);

        // When
        var createCostumerResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createCostumerResult.Should().NotBeNull();
        createCostumerResult.Id.Should().Be(costumer.Id);
        await _costumerRepository.Received(1).CreateAsync(costumer, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given invalid command When handling Then throws validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Given
        var command = new CreateCostumerCommand(); // Invalid command (empty fields)

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Trait("Category", "ApplicationCostumer")]
    [Fact(DisplayName = "Create Costumer - Given non-existent user When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentUser_ThrowsInvalidOperationException()
    {
        // Given
        var command = new CreateCostumerCommand
        {
            CostumerName = "Valid Costumer",
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