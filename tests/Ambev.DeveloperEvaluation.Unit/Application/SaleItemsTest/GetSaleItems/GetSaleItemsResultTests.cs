using System;
using Xunit;
using NSubstitute;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleItems.GetSaleItem
{
    public class GetSaleItemResultTests
    {
        [Fact]
        public void GetSaleItemResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var saleId = Guid.NewGuid();
            var saleDate = DateTimeOffset.UtcNow.AddDays(-10);
            var productId = Guid.NewGuid();
            decimal price = 123.59m;
            int quantity = 10;
            decimal discount = 235.90m;
            decimal totalPrice = price*quantity;
            var createAt = DateTimeOffset.UtcNow.AddDays(-15);
            var updateAt = DateTimeOffset.UtcNow.AddDays(-5);
            var deletedAt = DateTimeOffset.UtcNow.AddDays(-1);
            var statusItem = SaleItemStatus.NotCancelled;
            var userId = Guid.NewGuid();
            var totalWithDiscount = 1000.00m;

            // Act
            var result = new GetSaleItemResult()
            {
                Id = id,
                SaleId = saleId,
                SaleDate = saleDate,
                ProductId = productId,
                Price = price,
                Quantity = quantity,
                Discount = discount,
                TotalPrice = totalPrice,
                CreateAt = createAt,
                UpdateAt = updateAt,
                DeletedAt = deletedAt,
                StatusItem = statusItem,
                UserId = userId
            };

            // Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(saleId, result.SaleId);
            Assert.Equal(saleDate, result.SaleDate);
            Assert.Equal(productId, result.ProductId);
            Assert.Equal(price, result.Price);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(discount, result.Discount);
            Assert.Equal(totalPrice, result.TotalPrice);
            Assert.Equal(createAt, result.CreateAt);
            Assert.Equal(updateAt, result.UpdateAt);
            Assert.Equal(deletedAt, result.DeletedAt);
            Assert.Equal(statusItem, result.StatusItem);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(totalWithDiscount, result.TotalPriceWithDiscount);

        }

        [Fact]
        public void GetSaleItemResult_Should_HaveDefaultValues()
        {
            // Act
            var result = new GetSaleItemResult();

            // Assert
            Assert.Equal(Guid.Empty, result.Id);
            Assert.Equal(Guid.Empty, result.SaleId);
            Assert.Equal(DateTimeOffset.UtcNow.Date, result.SaleDate.Date);
            Assert.Equal(Guid.Empty, result.ProductId);
            Assert.Equal(default(decimal),result.Price);
            Assert.Equal(default(int),result.Quantity);
            Assert.Equal(default(decimal),result.Discount);
            Assert.Equal(default(decimal),result.TotalPrice);
            Assert.Equal(DateTimeOffset.UtcNow.Date, result.CreateAt.Date);
            Assert.Equal(DateTimeOffset.UtcNow.Date, result.UpdateAt.Date);
            Assert.Null(result.DeletedAt);
            Assert.Equal(Guid.Empty, result.UserId);
        }
    }
}