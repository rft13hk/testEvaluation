using Xunit;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductTest.GetAllProducts
{
    public class GetAllProductsCommandTests
    {
        [Trait("Category", "ApplicationProduct")]
        [Fact(DisplayName = "GetAllProducts - Given valid parameters When creating command Then command is created with default values")]
        public void GetAllProductsCommand_Should_HaveDefaultValues()
        {
            // Act
            var command = new GetAllProductsCommand();

            // Assert
            Assert.Equal(1, command.Page); // Default page is 1
            Assert.Equal(10, command.Size); // Default size is 10
            Assert.Null(command.Order); // Default order is null
            Assert.True(command.ActiveRecordsOnly); // Default is true
        }

        [Trait("Category", "ApplicationProduct")]
        [Fact(DisplayName = "GetAllProducts - GetAllProducts - Given valid parameters When creating command Then command is created with specified values")]
        public void GetAllProductsCommand_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var page = 2;
            var size = 20;
            var order = "ProductName desc";
            var activeRecordsOnly = false;

            // Act
            var command = new GetAllProductsCommand
            {
                Page = page,
                Size = size,
                Order = order,
                ActiveRecordsOnly = activeRecordsOnly
            };

            // Assert
            Assert.Equal(page, command.Page);
            Assert.Equal(size, command.Size);
            Assert.Equal(order, command.Order);
            Assert.False(command.ActiveRecordsOnly);
        }
    }
}