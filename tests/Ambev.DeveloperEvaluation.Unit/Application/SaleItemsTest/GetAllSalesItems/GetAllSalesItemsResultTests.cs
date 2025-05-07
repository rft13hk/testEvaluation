using System;
using System.Collections.Generic;
using Xunit;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetAllSaleItems;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItemTest.GetAllSaleItems
{
    public class GetAllSaleItemsResultTests
    {
        [Trait("Category", "ApplicationSaleItem")]
        [Fact(DisplayName = "GetAllSaleItems - Given valid parameters When creating result Then properties are set correctly")]
        public void GetAllSaleItemsResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branches = new List<GetAllSaleItemsResult.SaleItemDto>
            {
                new GetAllSaleItemsResult.SaleItemDto
                {
                    Id = Guid.NewGuid(),
                    SaleId = Guid.NewGuid(),
                    CreateAt = DateTimeOffset.UtcNow.AddDays(-10),
                    UpdateAt = DateTimeOffset.UtcNow.AddDays(-5),
                    DeletedAt = null,
                    UserId = Guid.NewGuid()
                },
                new GetAllSaleItemsResult.SaleItemDto
                {
                    Id = Guid.NewGuid(),
                    SaleId = Guid.NewGuid(),
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
            var result = new GetAllSaleItemsResult
            {
                SaleItems = branches,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage
            };

            // Assert
            Assert.Equal(branches, result.SaleItems);
            Assert.Equal(totalItems, result.TotalItems);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.Equal(currentPage, result.CurrentPage);
        }

        [Trait("Category", "ApplicationSaleItem")]
        [Fact(DisplayName = "GetAllSaleItems - Given valid parameters When creating SaleItemDto Then properties are set correctly")]
        public void SaleItemDto_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var createAt = DateTimeOffset.UtcNow.AddDays(-10);
            var updateAt = DateTimeOffset.UtcNow.AddDays(-5);
            var deletedAt = DateTimeOffset.UtcNow.AddDays(-1);
            var userId = Guid.NewGuid();

            // Act
            var branchDto = new GetAllSaleItemsResult.SaleItemDto
            {
                Id = branchId,
                SaleId = Guid.NewGuid(),
                CreateAt = createAt,
                UpdateAt = updateAt,
                DeletedAt = deletedAt,
                UserId = userId
            };

            // Assert
            Assert.Equal(branchId, branchDto.Id);
            Assert.Equal(createAt, branchDto.CreateAt);
            Assert.Equal(updateAt, branchDto.UpdateAt);
            Assert.Equal(deletedAt, branchDto.DeletedAt);
            Assert.Equal(userId, branchDto.UserId);
        }
    }
}