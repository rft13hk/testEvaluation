using Xunit;
using Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.GetAllBranches
{
    public class GetAllBranchesCommandTests
    {
        [Trait("Category", "ApplicationBranch")]
        [Fact(DisplayName = "GetAllBranches - Given valid parameters When creating command Then command is created with default values")]
        public void GetAllBranchesCommand_Should_HaveDefaultValues()
        {
            // Act
            var command = new GetAllBranchesCommand();

            // Assert
            Assert.Equal(1, command.Page); // Default page is 1
            Assert.Equal(10, command.Size); // Default size is 10
            Assert.Null(command.Order); // Default order is null
            Assert.True(command.ActiveRecordsOnly); // Default is true
        }

        [Trait("Category", "ApplicationBranch")]
        [Fact(DisplayName = "GetAllBranches - GetAllBranches - Given valid parameters When creating command Then command is created with specified values")]
        public void GetAllBranchesCommand_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var page = 2;
            var size = 20;
            var order = "BranchName desc";
            var activeRecordsOnly = false;

            // Act
            var command = new GetAllBranchesCommand
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