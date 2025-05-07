using Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.Shared;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.DeleteSaleItem;

/// <summary>
/// Unit tests for the <see cref="DeleteSaleItemHandler"/> class.
/// </summary>
public class DeleteSaleItemHandlerTest
{
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly DeleteSaleItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleItemHandlerTest"/> class.
    /// </summary>
    public DeleteSaleItemHandlerTest()
    {
        _saleItemRepository = Substitute.For<ISaleItemRepository>();
        _handler = new DeleteSaleItemHandler(_saleItemRepository);
    }


    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Delete SaleItem - Given valid branch ID When handling Then deletes branch successfully")]
    public async Task Handle_ValidSaleItemId_DeletesSaleItemSuccessfully()
    {
        // Given
        var _saleItemsScenariosTest = new SaleItemsScenariosTest();
        var saleItem = await _saleItemsScenariosTest.CreateFackeSaleItem();

        _saleItemRepository.GetByIdAsync(saleItem.Id, Arg.Any<CancellationToken>())
            .Returns(saleItem);
            
        _saleItemRepository.DeleteAsync(saleItem.Id, Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        await _handler.Handle(new DeleteSaleItemCommand(saleItem.Id), CancellationToken.None);

        // Then
        await _saleItemRepository.Received(1).DeleteAsync(saleItem.Id, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationSaleItem")]
    [Fact(DisplayName = "Delete SaleItem - Given non-existent branch ID When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentSaleItemId_ThrowsInvalidOperationException()
    {
        // Given
        var branchId = Guid.NewGuid();
        _saleItemRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns((Ambev.DeveloperEvaluation.Domain.Entities.SaleItem)null!);

        // When
        Func<Task> act = async () => await _handler.Handle(new DeleteSaleItemCommand(branchId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"SaleItem with ID {branchId} not found.");
    }
}