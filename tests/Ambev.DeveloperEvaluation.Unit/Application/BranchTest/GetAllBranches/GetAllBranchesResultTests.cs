using System;
using System.Collections.Generic;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.GetAllBranches
{
    public class GetAllBranchesResultTests
    {
        [Trait("Category", "ApplicationBranch")]
        [Fact(DisplayName = "GetAllBranches - Given valid parameters When creating result Then properties are set correctly")]
        public void GetAllBranchesResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branches = new List<GetAllBranchesResult.BranchDto>
            {
                new GetAllBranchesResult.BranchDto
                {
                    Id = Guid.NewGuid(),
                    BranchName = "Branch 1",
                    CreateAt = DateTimeOffset.UtcNow.AddDays(-10),
                    UpdateAt = DateTimeOffset.UtcNow.AddDays(-5),
                    DeletedAt = null,
                    UserId = Guid.NewGuid(),
                    User = null
                },
                new GetAllBranchesResult.BranchDto
                {
                    Id = Guid.NewGuid(),
                    BranchName = "Branch 2",
                    CreateAt = DateTimeOffset.UtcNow.AddDays(-20),
                    UpdateAt = DateTimeOffset.UtcNow.AddDays(-15),
                    DeletedAt = DateTimeOffset.UtcNow.AddDays(-1),
                    UserId = Guid.NewGuid(),
                    User = null
                }
            };

            var totalItems = 50;
            var totalPages = 5;
            var currentPage = 2;

            // Act
            var result = new GetAllBranchesResult
            {
                Branches = branches,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage
            };

            // Assert
            Assert.Equal(branches, result.Branches);
            Assert.Equal(totalItems, result.TotalItems);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.Equal(currentPage, result.CurrentPage);
        }

        [Trait("Category", "ApplicationBranch")]
        [Fact(DisplayName = "GetAllBranches - Given valid parameters When creating BranchDto Then properties are set correctly")]
        public void BranchDto_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "Test Branch";
            var createAt = DateTimeOffset.UtcNow.AddDays(-10);
            var updateAt = DateTimeOffset.UtcNow.AddDays(-5);
            var deletedAt = DateTimeOffset.UtcNow.AddDays(-1);
            var userId = Guid.NewGuid();

            // Act
            var branchDto = new GetAllBranchesResult.BranchDto
            {
                Id = branchId,
                BranchName = branchName,
                CreateAt = createAt,
                UpdateAt = updateAt,
                DeletedAt = deletedAt,
                UserId = userId,
                User = null
            };

            // Assert
            Assert.Equal(branchId, branchDto.Id);
            Assert.Equal(branchName, branchDto.BranchName);
            Assert.Equal(createAt, branchDto.CreateAt);
            Assert.Equal(updateAt, branchDto.UpdateAt);
            Assert.Equal(deletedAt, branchDto.DeletedAt);
            Assert.Equal(userId, branchDto.UserId);
            Assert.Null(branchDto.User);
        }
    }
}