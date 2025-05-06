using Xunit;
using Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.GetAllCostumers
{
    public class GetAllCostumersCommandTests
    {
        [Trait("Category", "ApplicationCostumer")]
        [Fact(DisplayName = "GetAllCostumers - Given valid parameters When creating command Then command is created with default values")]
        public void GetAllCostumersCommand_Should_HaveDefaultValues()
        {
            // Act
            var command = new GetAllCostumersCommand();

            // Assert
            Assert.Equal(1, command.Page); // Default page is 1
            Assert.Equal(10, command.Size); // Default size is 10
            Assert.Null(command.Order); // Default order is null
            Assert.True(command.ActiveRecordsOnly); // Default is true
        }

        [Trait("Category", "ApplicationCostumer")]
        [Fact(DisplayName = "GetAllCostumers - GetAllCostumers - Given valid parameters When creating command Then command is created with specified values")]
        public void GetAllCostumersCommand_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var page = 2;
            var size = 20;
            var order = "CostumerName desc";
            var activeRecordsOnly = false;

            // Act
            var command = new GetAllCostumersCommand
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