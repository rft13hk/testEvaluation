using System;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetSale
{
    public class GetSaleResultTests
    {
        [Fact]
        public void GetSaleResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var saleNumber = "SALE123";
            var saleDate = DateTimeOffset.UtcNow.AddDays(-10);
            var costumerId = Guid.NewGuid();
            var branchId = Guid.NewGuid();
            var createAt = DateTimeOffset.UtcNow.AddDays(-15);
            var updateAt = DateTimeOffset.UtcNow.AddDays(-5);
            var deletedAt = DateTimeOffset.UtcNow.AddDays(-1);
            var userId = Guid.NewGuid();
            var totalItems = 5;
            var totalValue = 100.50m;
            var totalDiscount = 10.50m;
            var totalWithDiscount = 90.00m;

            // Act
            var result = new GetSaleResult
            {
                Id = saleId,
                SaleNumber = saleNumber,
                SaleDate = saleDate,
                CostumerId = costumerId,
                BranchId = branchId,
                CreateAt = createAt,
                UpdateAt = updateAt,
                DeletedAt = deletedAt,
                UserId = userId,
                TotalItems = totalItems,
                TotalValue = totalValue,
                TotalDiscount = totalDiscount,
                TotalWithDiscount = totalWithDiscount
            };

            // Assert
            Assert.Equal(saleId, result.Id);
            Assert.Equal(saleNumber, result.SaleNumber);
            Assert.Equal(saleDate, result.SaleDate);
            Assert.Equal(costumerId, result.CostumerId);
            Assert.Equal(branchId, result.BranchId);
            Assert.Equal(createAt, result.CreateAt);
            Assert.Equal(updateAt, result.UpdateAt);
            Assert.Equal(deletedAt, result.DeletedAt);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(totalItems, result.TotalItems);
            Assert.Equal(totalValue, result.TotalValue);
            Assert.Equal(totalDiscount, result.TotalDiscount);
            Assert.Equal(totalWithDiscount, result.TotalWithDiscount);
        }

        [Fact]
        public void GetSaleResult_Should_HaveDefaultValues()
        {
            // Act
            var result = new GetSaleResult();

            // Assert
            Assert.Equal(Guid.Empty, result.Id);
            Assert.Equal(string.Empty, result.SaleNumber);
            Assert.Equal(DateTimeOffset.UtcNow.Date, result.SaleDate.Date);
            Assert.Equal(Guid.Empty, result.CostumerId);
            Assert.Equal(Guid.Empty, result.BranchId);
            Assert.Equal(DateTimeOffset.UtcNow.Date, result.CreateAt.Date);
            Assert.Equal(DateTimeOffset.UtcNow.Date, result.UpdateAt.Date);
            Assert.Null(result.DeletedAt);
            Assert.Equal(Guid.Empty, result.UserId);
            Assert.Equal(0, result.TotalItems);
            Assert.Equal(0.0m, result.TotalValue);
            Assert.Equal(0.0m, result.TotalDiscount);
            Assert.Equal(0.0m, result.TotalWithDiscount);
        }
    }
}