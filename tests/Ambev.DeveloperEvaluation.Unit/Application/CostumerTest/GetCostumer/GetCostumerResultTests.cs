using Xunit;
using Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;
using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.GetCostumer;

    public class GetCostumerResultTests
    {
        [Fact]
        public void GetCostumerResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "Test Costumer";
            var createAt = DateTimeOffset.UtcNow;
            var updateAt = DateTimeOffset.UtcNow;

            // Act
            var result = new GetCostumerResult
            {
                Id = branchId,
                CostumerName = branchName,
                CreateAt = createAt,
                UpdateAt = updateAt
            };

            // Assert
            Assert.Equal(branchId, result.Id);
            Assert.Equal(branchName, result.CostumerName);
            Assert.Equal(createAt, result.CreateAt);
            Assert.Equal(updateAt, result.UpdateAt);
        }

        [Fact]
        public void GetCostumerResult_Should_AllowNullUser()
        {
            // Arrange
            var result = new GetCostumerResult();

            // Act
            result.User = null;

            // Assert
            Assert.Null(result.User);
        }

        [Fact]
        public void GetCostumerResult_Should_SetUserCorrectly()
        {
            // Arrange
            var user = new GetBranchResultUser
            {
                Id = Guid.NewGuid(),
                Username = "TestUser",
                Email = "test@example.com",
                CreatedAt = DateTime.UtcNow,
            };

            // Act
            var result = new GetCostumerResult
            {
                User = new DeveloperEvaluation.Domain.Entities.User
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                }
            };

            // Assert
            Assert.NotNull(result.User);
            Assert.Equal(user.Id, result.User.Id);
            Assert.Equal(user.Username, result.User.Username);
            Assert.Equal(user.Email, result.User.Email);
        }
    }
