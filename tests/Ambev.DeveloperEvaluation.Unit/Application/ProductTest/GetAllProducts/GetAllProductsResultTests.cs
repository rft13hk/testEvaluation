using System;
using System.Collections.Generic;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.GetAllProducts
{
    public class GetAllProductsResultTests
    {
        [Trait("Category", "ApplicationProduct")]
        [Fact(DisplayName = "GetAllProducts - Given valid parameters When creating result Then properties are set correctly")]
        public void GetAllProductsResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branches = new List<GetAllProductsResult.ProductDto>
            {
                new GetAllProductsResult.ProductDto
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Product 1",
                    CreateAt = DateTimeOffset.UtcNow.AddDays(-10),
                    UpdateAt = DateTimeOffset.UtcNow.AddDays(-5),
                    DeletedAt = null,
                    UserId = Guid.NewGuid()
                },
                new GetAllProductsResult.ProductDto
                {
                    Id = Guid.NewGuid(),
                    ProductName = "Product 2",
                    CreateAt = DateTimeOffset.UtcNow.AddDays(-20),
                    UpdateAt = DateTimeOffset.UtcNow.AddDays(-15),
                    DeletedAt = DateTimeOffset.UtcNow.AddDays(-1),
                    UserId = Guid.NewGuid()
                }
            };

            var totalItems = 50;
            var totalPages = 5;
            var currentPage = 2;

            // Act
            var result = new GetAllProductsResult
            {
                Products = branches,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage
            };

            // Assert
            Assert.Equal(branches, result.Products);
            Assert.Equal(totalItems, result.TotalItems);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.Equal(currentPage, result.CurrentPage);
        }

        [Trait("Category", "ApplicationProduct")]
        [Fact(DisplayName = "GetAllProducts - Given valid parameters When creating ProductDto Then properties are set correctly")]
        public void ProductDto_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "Test Product";
            var createAt = DateTimeOffset.UtcNow.AddDays(-10);
            var updateAt = DateTimeOffset.UtcNow.AddDays(-5);
            var deletedAt = DateTimeOffset.UtcNow.AddDays(-1);
            var userId = Guid.NewGuid();

            // Act
            var branchDto = new GetAllProductsResult.ProductDto
            {
                Id = branchId,
                ProductName = branchName,
                CreateAt = createAt,
                UpdateAt = updateAt,
                DeletedAt = deletedAt,
                UserId = userId
            };

            // Assert
            Assert.Equal(branchId, branchDto.Id);
            Assert.Equal(branchName, branchDto.ProductName);
            Assert.Equal(createAt, branchDto.CreateAt);
            Assert.Equal(updateAt, branchDto.UpdateAt);
            Assert.Equal(deletedAt, branchDto.DeletedAt);
            Assert.Equal(userId, branchDto.UserId);
        }
    }
}