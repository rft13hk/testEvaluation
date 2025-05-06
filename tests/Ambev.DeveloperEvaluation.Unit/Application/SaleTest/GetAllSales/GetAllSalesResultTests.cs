using System;
using System.Collections.Generic;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleTest.GetAllSales
{
    public class GetAllSalesResultTests
    {
        [Trait("Category", "ApplicationSale")]
        [Fact(DisplayName = "GetAllSales - Given valid parameters When creating result Then properties are set correctly")]
        public void GetAllSalesResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branches = new List<GetAllSalesResult.SaleDto>
            {
                new GetAllSalesResult.SaleDto
                {
                    Id = Guid.NewGuid(),
                    SaleNumber = "Sale 1",
                    CreateAt = DateTimeOffset.UtcNow.AddDays(-10),
                    UpdateAt = DateTimeOffset.UtcNow.AddDays(-5),
                    DeletedAt = null,
                    UserId = Guid.NewGuid()
                },
                new GetAllSalesResult.SaleDto
                {
                    Id = Guid.NewGuid(),
                    SaleNumber = "Sale 2",
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
            var result = new GetAllSalesResult
            {
                Sales = branches,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage
            };

            // Assert
            Assert.Equal(branches, result.Sales);
            Assert.Equal(totalItems, result.TotalItems);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.Equal(currentPage, result.CurrentPage);
        }

        [Trait("Category", "ApplicationSale")]
        [Fact(DisplayName = "GetAllSales - Given valid parameters When creating SaleDto Then properties are set correctly")]
        public void SaleDto_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "Test Sale";
            var createAt = DateTimeOffset.UtcNow.AddDays(-10);
            var updateAt = DateTimeOffset.UtcNow.AddDays(-5);
            var deletedAt = DateTimeOffset.UtcNow.AddDays(-1);
            var userId = Guid.NewGuid();

            // Act
            var branchDto = new GetAllSalesResult.SaleDto
            {
                Id = branchId,
                SaleNumber = branchName,
                CreateAt = createAt,
                UpdateAt = updateAt,
                DeletedAt = deletedAt,
                UserId = userId
            };

            // Assert
            Assert.Equal(branchId, branchDto.Id);
            Assert.Equal(branchName, branchDto.SaleNumber);
            Assert.Equal(createAt, branchDto.CreateAt);
            Assert.Equal(updateAt, branchDto.UpdateAt);
            Assert.Equal(deletedAt, branchDto.DeletedAt);
            Assert.Equal(userId, branchDto.UserId);
        }
    }
}