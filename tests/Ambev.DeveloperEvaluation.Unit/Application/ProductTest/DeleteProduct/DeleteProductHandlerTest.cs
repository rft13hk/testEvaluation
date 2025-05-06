using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.DeleteProduct;

/// <summary>
/// Unit tests for the <see cref="DeleteProductHandler"/> class.
/// </summary>
public class DeleteProductHandlerTest
{
    private readonly IProductRepository _branchRepository;
    private readonly DeleteProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProductHandlerTest"/> class.
    /// </summary>
    public DeleteProductHandlerTest()
    {
        _branchRepository = Substitute.For<IProductRepository>();
        _handler = new DeleteProductHandler(_branchRepository);
    }


    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Delete Product - Given valid branch ID When handling Then deletes branch successfully")]
    public async Task Handle_ValidProductId_DeletesProductSuccessfully()
    {
        // Given
        var branchId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        _branchRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns(new Ambev.DeveloperEvaluation.Domain.Entities.Product() 
            { 
                Id = branchId,
                UserId = userId,
                User = new Ambev.DeveloperEvaluation.Domain.Entities.User()
                {
                    Id = userId
                } // Initialize the required Users property
            });
            
        _branchRepository.DeleteAsync(branchId, Arg.Any<CancellationToken>())
            .Returns(true);


        // When
        await _handler.Handle(new DeleteProductCommand(branchId), CancellationToken.None);

        // Then
        await _branchRepository.Received(1).DeleteAsync(branchId, Arg.Any<CancellationToken>());
    }

    [Trait("Category", "ApplicationProduct")]
    [Fact(DisplayName = "Delete Product - Given non-existent branch ID When handling Then throws invalid operation exception")]
    public async Task Handle_NonExistentProductId_ThrowsInvalidOperationException()
    {
        // Given
        var branchId = Guid.NewGuid();
        _branchRepository.GetByIdAsync(branchId, Arg.Any<CancellationToken>())
            .Returns((Ambev.DeveloperEvaluation.Domain.Entities.Product)null!);

        // When
        Func<Task> act = async () => await _handler.Handle(new DeleteProductCommand(branchId), CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Product with ID {branchId} not found.");
    }
}