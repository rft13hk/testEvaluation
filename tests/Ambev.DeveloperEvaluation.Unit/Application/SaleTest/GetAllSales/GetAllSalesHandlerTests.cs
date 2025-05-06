using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.GetAllSales
{
    public class GetAllSalesHandlerTests
    {
        private readonly ISaleItemRepository _SaleItemRepository;
        private readonly ISaleRepository _mockSaleRepository;
        private readonly IMapper _mockMapper;
        private readonly GetAllSalesHandler _handler;

        public GetAllSalesHandlerTests()
        {
            _mockSaleRepository = Substitute.For<ISaleRepository>();
            _SaleItemRepository = Substitute.For<ISaleItemRepository>();
            _mockMapper = Substitute.For<IMapper>();

            _handler = new GetAllSalesHandler(_mockSaleRepository, _SaleItemRepository, _mockMapper);
        }

        [Trait("Category", "ApplicationSale")]
        [Fact(DisplayName = "GetAllSales - Handle - Should return paginated result when branches exist")]
        public async Task Handle_Should_ReturnPaginatedResult_When_SalesExist()
        {
            // Arrange
            var costumerId = Guid.NewGuid();
            var BrancheId = Guid.NewGuid();
            var user = new Ambev.DeveloperEvaluation.Domain.Entities.User()
            {
                Id = Guid.NewGuid(),
                Username = "User"
            };


            var sales = new List<Ambev.DeveloperEvaluation.Domain.Entities.Sale>
            {
                new Ambev.DeveloperEvaluation.Domain.Entities.Sale 
                { 
                    Id = Guid.NewGuid(),
                    SaleNumber = "001",
                    UserId = user.Id,
                    User = user,
                    CostumerId = costumerId,
                    Costumer = new DeveloperEvaluation.Domain.Entities.Costumer()
                    {
                        Id = costumerId,
                        UserId = user.Id,
                        Users = user
                    },
                    BranchId = BrancheId,
                    Branch = new DeveloperEvaluation.Domain.Entities.Branch()
                    {
                        Id = BrancheId,
                        UserId = user.Id,
                        User = user
                    },
                    CreateAt = DateTimeOffset.UtcNow,
                    UpdateAt = DateTimeOffset.UtcNow                
                },
                new Ambev.DeveloperEvaluation.Domain.Entities.Sale 
                { 
                    Id = Guid.NewGuid(),
                    SaleNumber = "002",
                    UserId = user.Id,
                    User = user,
                    CostumerId = costumerId,
                    Costumer = new DeveloperEvaluation.Domain.Entities.Costumer()
                    {
                        Id = costumerId,
                        UserId = user.Id,
                        Users = user
                    },
                    BranchId = BrancheId,
                    Branch = new DeveloperEvaluation.Domain.Entities.Branch()
                    {
                        Id = BrancheId,
                        UserId = user.Id,
                        User = user
                    },
                    CreateAt = DateTimeOffset.UtcNow,
                    UpdateAt = DateTimeOffset.UtcNow                

                }
            };

            var branchDtos = sales.Select(b => new GetAllSalesResult.SaleDto
            {
                Id = b.Id,
                SaleNumber = b.SaleNumber,
                CreateAt = b.CreateAt,
                UpdateAt = b.UpdateAt
            });

            _mockSaleRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.Sale>)sales));

            _mockSaleRepository.GetTotalSalesCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(2));

            _mockMapper.Map<IEnumerable<GetAllSalesResult.SaleDto>>(sales)
                .Returns(branchDtos);

            var command = new GetAllSalesCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalItems);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(branchDtos, result.Sales);
        }

        [Trait("Category", "ApplicationSale")]
        [Fact(DisplayName = "GetAllSales - Handle - Should return empty result when no branches exist")]
        public async Task Handle_Should_ReturnEmptyResult_When_NoSalesExist()
        {

            var branchDtos = Enumerable.Empty<DeveloperEvaluation.Domain.Entities.Sale>();

            // Arrange
            _mockSaleRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.Sale>)branchDtos));

            _mockSaleRepository.GetTotalSalesCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(0));

            var command = new GetAllSalesCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Sales);
            Assert.Equal(0, result.TotalItems);
            Assert.Equal(0, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
        }

        [Trait("Category", "ApplicationSale")]
        [Fact(DisplayName = "GetAllSales - Handle - Should call repository with correct parameters")]
        public async Task Handle_Should_CallRepository_WithCorrectParameters()
        {
            // Arrange
            var command = new GetAllSalesCommand { Page = 2, Size = 5, Order = "Name asc", ActiveRecordsOnly = false };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _mockSaleRepository.Received(1).GetAllAsync(2, 5, "Name asc", false, Arg.Any<CancellationToken>());
            await _mockSaleRepository.Received(1).GetTotalSalesCountAsync(false, Arg.Any<CancellationToken>());
        }
    }
}