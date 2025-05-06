using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.GetAllProducts
{
    public class GetAllProductsHandlerTests
    {
        private readonly IProductRepository _mockProductRepository;
        private readonly IMapper _mockMapper;
        private readonly GetAllProductsHandler _handler;

        public GetAllProductsHandlerTests()
        {
            _mockProductRepository = Substitute.For<IProductRepository>();
            _mockMapper = Substitute.For<IMapper>();
            _handler = new GetAllProductsHandler(_mockProductRepository, _mockMapper);
        }

        [Trait("Category", "ApplicationProduct")]
        [Fact(DisplayName = "GetAllProducts - Handle - Should return paginated result when branches exist")]
        public async Task Handle_Should_ReturnPaginatedResult_When_ProductsExist()
        {
            // Arrange
            var branches = new List<Ambev.DeveloperEvaluation.Domain.Entities.Product>
            {
                new Ambev.DeveloperEvaluation.Domain.Entities.Product 
                { 
                    Id = Guid.NewGuid(), 
                    ProductName = "Product 1", 
                    CreateAt = DateTimeOffset.UtcNow, 
                    UpdateAt = DateTimeOffset.UtcNow,
                    UserId = Guid.NewGuid(),
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "User 1",
                        Email = "user1@example.com"
                    }
                },
                new Ambev.DeveloperEvaluation.Domain.Entities.Product 
                { 
                    Id = Guid.NewGuid(), 
                    ProductName = "Product 2", 
                    CreateAt = DateTimeOffset.UtcNow, 
                    UpdateAt = DateTimeOffset.UtcNow,
                    UserId = Guid.NewGuid(),
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "User 1",
                        Email = "user1@example.com"
                    }

                }
            };

            var branchDtos = branches.Select(b => new GetAllProductsResult.ProductDto
            {
                Id = b.Id,
                ProductName = b.ProductName,
                CreateAt = b.CreateAt,
                UpdateAt = b.UpdateAt
            });

            _mockProductRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.Product>)branches));

            _mockProductRepository.GetTotalProductsCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(2));

            _mockMapper.Map<IEnumerable<GetAllProductsResult.ProductDto>>(branches)
                .Returns(branchDtos);

            var command = new GetAllProductsCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalItems);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(branchDtos, result.Products);
        }

        [Trait("Category", "ApplicationProduct")]
        [Fact(DisplayName = "GetAllProducts - Handle - Should return empty result when no branches exist")]
        public async Task Handle_Should_ReturnEmptyResult_When_NoProductsExist()
        {

            var branchDtos = Enumerable.Empty<DeveloperEvaluation.Domain.Entities.Product>();

            // Arrange
            _mockProductRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.Product>)branchDtos));

            _mockProductRepository.GetTotalProductsCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(0));

            var command = new GetAllProductsCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Products);
            Assert.Equal(0, result.TotalItems);
            Assert.Equal(0, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
        }

        [Trait("Category", "ApplicationProduct")]
        [Fact(DisplayName = "GetAllProducts - Handle - Should call repository with correct parameters")]
        public async Task Handle_Should_CallRepository_WithCorrectParameters()
        {
            // Arrange
            var command = new GetAllProductsCommand { Page = 2, Size = 5, Order = "Name asc", ActiveRecordsOnly = false };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _mockProductRepository.Received(1).GetAllAsync(2, 5, "Name asc", false, Arg.Any<CancellationToken>());
            await _mockProductRepository.Received(1).GetTotalProductsCountAsync(false, Arg.Any<CancellationToken>());
        }
    }
}