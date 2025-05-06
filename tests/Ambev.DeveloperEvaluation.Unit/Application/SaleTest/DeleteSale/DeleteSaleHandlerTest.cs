using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.DeleteSale;

/// <summary>
/// Unit tests for the <see cref="DeleteSaleHandler"/> class.
/// </summary>
public class DeleteSaleHandlerTest
{
    private readonly ISaleRepository _branchRepository;
    private readonly DeleteSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandlerTest"/> class.
    /// </summary>
    public DeleteSaleHandlerTest()
    {
        _branchRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleHandler(_branchRepository);
    }


    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Delete Sale - Given valid branch ID When handling Then deletes branch successfully")]
    public async Task Handle_ValidSaleId_DeletesSaleSuccessfully()
    {
        // Given
        var branchId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var consumerId = Guid.NewGuid();

        _branchRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns(new Ambev.DeveloperEvaluation.Domain.Entities.Sale() 
            { 
                Id = Guid.NewGuid(),
                SaleNumber = "001",
                UserId = userId,
                User = new User { Id = userId },
                CostumerId = consumerId,
                Costumer = new DeveloperEvaluation.Domain.Entities.Costumer()
                {
                    Id = consumerId,
                    UserId = userId,
                    Users = new User()
                    {
                        Id = userId
                    }
                },
                BranchId = branchId,
                Branch = new DeveloperEvaluation.Domain.Entities.Branch()
                {
                    UserId = userId,
                    User = new User(){ Id = userId},
                    BranchName = "brantTest"
                }
            });
            
        _branchRepository.DeleteAsync(branchId, Arg.Any<CancellationToken>())
            .Returns(true);


        // When
        await _handler.Handle(new DeleteSaleCommand(branchId), CancellationToken.None);

        // Then
        await _branchRepository.Received(1).DeleteAsync(branchId, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationSale")]
    [Fact(DisplayName = "Delete Sale - Given non-existent branch ID When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentSaleId_ThrowsInvalidOperationException()
    {
        // Given
        var branchId = Guid.NewGuid();
        _branchRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns((Ambev.DeveloperEvaluation.Domain.Entities.Sale)null!);

        // When
        Func<Task> act = async () => await _handler.Handle(new DeleteSaleCommand(branchId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {branchId} not found.");
    }
}