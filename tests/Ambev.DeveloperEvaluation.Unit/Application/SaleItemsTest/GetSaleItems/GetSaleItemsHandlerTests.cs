using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.Shared;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.GetSaleItem;

public class GetSaleItemHandlerTests
{
    private readonly ISaleItemRepository _mockRepository;
    private readonly GetSaleItemHandler _handler;
    //private readonly ISaleItemRepository _branchRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetSaleItemHandlerTests()
    {
        // Mock do repositório
        _mockRepository = Substitute.For<ISaleItemRepository>();
        _mapper = Substitute.For<IMapper>();
        _userRepository = Substitute.For<IUserRepository>();
        _handler = new GetSaleItemHandler(_mockRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnSaleItemResult_When_SaleItemExists()
    {
        // Arrange
        var _saleItemsScenariosTest = new SaleItemsScenariosTest();

        var fackeUser = await _saleItemsScenariosTest.CreateFackeUser();

        var fackeCostumer = await _saleItemsScenariosTest.CreateFackeCostumer(fackeUser);

        var fackeBranche = await _saleItemsScenariosTest.CreateFackeBranch(fackeUser);

        var fackeSale = await _saleItemsScenariosTest.CreateFackeSale(fackeCostumer,fackeBranche, fackeUser);

        var fackeProduct = await _saleItemsScenariosTest.CreateFackeProduct(fackeUser);

        var fackeSaleItem = await _saleItemsScenariosTest.CreateFackeSaleItem(fackeSale, fackeProduct, fackeUser);

        var resultSaleItem = new GetSaleItemResult
        {
            SaleId = fackeSaleItem.SaleId,
            Id = fackeSaleItem.Id
        };

        _mockRepository.GetByIdAsync(fackeSaleItem.Id)!.Returns(Task.FromResult(fackeSaleItem));
        _mapper.Map<GetSaleItemResult>(fackeSaleItem).Returns(resultSaleItem);        
        var query = new GetSaleItemCommand(fackeSaleItem.Id);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);


        // Assert
        Assert.NotNull(result);
        Assert.Equal(fackeSaleItem.Id, result.Id);
        Assert.Equal(fackeSaleItem.SaleId, result.SaleId);

    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_SaleItemDoesNotExist()
    {
        // Arrange
        var branchId = Guid.NewGuid();
        _mockRepository.GetByIdAsync(branchId)!.Returns(Task.FromResult<Ambev.DeveloperEvaluation.Domain.Entities.SaleItem>(null!));

        var query = new GetSaleItemCommand(branchId);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }


}
