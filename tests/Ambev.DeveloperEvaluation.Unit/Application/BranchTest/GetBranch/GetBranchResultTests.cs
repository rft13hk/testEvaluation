using Xunit;
using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.GetBranch;

    public class GetBranchResultTests
    {
        [Fact]
        public void GetBranchResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "Test Branch";
            var createAt = DateTimeOffset.UtcNow;
            var updateAt = DateTimeOffset.UtcNow;

            // Act
            var result = new GetBranchResult
            {
                Id = branchId,
                BranchName = branchName,
                CreateAt = createAt,
                UpdateAt = updateAt
            };

            // Assert
            Assert.Equal(branchId, result.Id);
            Assert.Equal(branchName, result.BranchName);
            Assert.Equal(createAt, result.CreateAt);
            Assert.Equal(updateAt, result.UpdateAt);
        }

        [Fact]
        public void GetBranchResult_Should_AllowNullUser()
        {
            // Arrange
            var result = new GetBranchResult();

            // Act
            result.User = null;

            // Assert
            Assert.Null(result.User);
        }

        [Fact]
        public void GetBranchResult_Should_SetUserCorrectly()
        {
            // Arrange
            var user = new GetBranchResultUser
            {
                Id = Guid.NewGuid(),
                Username = "TestUser",
                Email = "test@example.com"
            };

            // Act
            var result = new GetBranchResult
            {
                User = user
            };

            // Assert
            Assert.NotNull(result.User);
            Assert.Equal(user.Id, result.User.Id);
            Assert.Equal(user.Username, result.User.Username);
            Assert.Equal(user.Email, result.User.Email);
        }
    }
