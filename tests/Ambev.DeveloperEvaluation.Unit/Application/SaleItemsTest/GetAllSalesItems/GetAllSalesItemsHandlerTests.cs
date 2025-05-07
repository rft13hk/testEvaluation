using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetAllSaleItems;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.Shared;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.GetAllSaleItems
{
    public class GetAllSaleItemsHandlerTests
    {
        private readonly ISaleItemRepository _mockSaleItemRepository;
        
        private readonly ISaleRepository _mockSaleRepository;
        private readonly IMapper _mockMapper;
        private readonly GetAllSaleItemsHandler _handler;

        public GetAllSaleItemsHandlerTests()
        {
            _mockSaleRepository = Substitute.For<ISaleRepository>();
            _mockSaleItemRepository = Substitute.For<ISaleItemRepository>();
            _mockMapper = Substitute.For<IMapper>();

            _handler = new GetAllSaleItemsHandler(_mockSaleItemRepository, _mockMapper);
        }


        [Trait("Category", "ApplicationSaleItem")]
        [Fact(DisplayName = "GetAllSaleItems - Handle - Should return paginated result when branches exist")]
        public async Task Handle_Should_ReturnPaginatedResult_When_SaleItemsExist()
        {
            // Arrange
            var costumerId = Guid.NewGuid();
            var BrancheId = Guid.NewGuid();
            var user = new Ambev.DeveloperEvaluation.Domain.Entities.User()
            {
                Id = Guid.NewGuid(),
                Username = "User"
            };

            var scenarios = new SaleItemsScenariosTest();

            var saleItems = new List<Ambev.DeveloperEvaluation.Domain.Entities.SaleItem>();

            saleItems.Add(await scenarios.CreateFackeSaleItem("001", "user01", "costumer01", "branch01", "001", "product001", 123.45m, 1));
            saleItems.Add(await scenarios.CreateFackeSaleItem("001", "user01", "costumer01", "branch01", "002", "product002", 123.45m, 5));

            var branchDtos = saleItems.Select(b => new GetAllSaleItemsResult.SaleItemDto
            {
                SaleId = b.SaleId,              
                Id = b.Id,
                SaleDate = b.SaleDate,
                CreateAt = b.CreateAt,
                UpdateAt = b.UpdateAt
            });

            _mockSaleItemRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.SaleItem>)saleItems));

            _mockSaleItemRepository.GetTotalSaleItemsCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(2));

            _mockMapper.Map<IEnumerable<GetAllSaleItemsResult.SaleItemDto>>(saleItems)
                .Returns(branchDtos);

            var command = new GetAllSaleItemsCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalItems);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(branchDtos, result.SaleItems);
        }

        [Trait("Category", "ApplicationSaleItem")]
        [Fact(DisplayName = "GetAllSaleItems - Handle - Should return empty result when no branches exist")]
        public async Task Handle_Should_ReturnEmptyResult_When_NoSaleItemsExist()
        {

            var branchDtos = Enumerable.Empty<DeveloperEvaluation.Domain.Entities.SaleItem>();

            // Arrange
            _mockSaleItemRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.SaleItem>)branchDtos));

            _mockSaleItemRepository.GetTotalSaleItemsCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(0));

            var command = new GetAllSaleItemsCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.SaleItems);
            Assert.Equal(0, result.TotalItems);
            Assert.Equal(0, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
        }

        [Trait("Category", "ApplicationSaleItem")]
        [Fact(DisplayName = "GetAllSaleItems - Handle - Should call repository with correct parameters")]
        public async Task Handle_Should_CallRepository_WithCorrectParameters()
        {
            // Arrange
            var command = new GetAllSaleItemsCommand { Page = 2, Size = 5, Order = "Name asc", ActiveRecordsOnly = false };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _mockSaleItemRepository.Received(1).GetAllAsync(2, 5, "Name asc", false, Arg.Any<CancellationToken>());
            await _mockSaleItemRepository.Received(1).GetTotalSaleItemsCountAsync(false, Arg.Any<CancellationToken>());
        }
    }
}