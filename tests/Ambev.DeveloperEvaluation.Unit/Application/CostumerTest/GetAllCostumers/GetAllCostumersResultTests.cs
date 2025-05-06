using System;
using System.Collections.Generic;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.GetAllCostumers
{
    public class GetAllCostumersResultTests
    {
        [Trait("Category", "ApplicationCostumer")]
        [Fact(DisplayName = "GetAllCostumers - Given valid parameters When creating result Then properties are set correctly")]
        public void GetAllCostumersResult_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branches = new List<GetAllCostumersResult.CostumerDto>
            {
                new GetAllCostumersResult.CostumerDto
                {
                    Id = Guid.NewGuid(),
                    CostumerName = "Costumer 1",
                    CreateAt = DateTimeOffset.UtcNow.AddDays(-10),
                    UpdateAt = DateTimeOffset.UtcNow.AddDays(-5),
                    DeletedAt = null,
                    UserId = Guid.NewGuid()
                },
                new GetAllCostumersResult.CostumerDto
                {
                    Id = Guid.NewGuid(),
                    CostumerName = "Costumer 2",
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
            var result = new GetAllCostumersResult
            {
                Costumers = branches,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage
            };

            // Assert
            Assert.Equal(branches, result.Costumers);
            Assert.Equal(totalItems, result.TotalItems);
            Assert.Equal(totalPages, result.TotalPages);
            Assert.Equal(currentPage, result.CurrentPage);
        }

        [Trait("Category", "ApplicationCostumer")]
        [Fact(DisplayName = "GetAllCostumers - Given valid parameters When creating CostumerDto Then properties are set correctly")]
        public void CostumerDto_Should_SetAndGetPropertiesCorrectly()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "Test Costumer";
            var createAt = DateTimeOffset.UtcNow.AddDays(-10);
            var updateAt = DateTimeOffset.UtcNow.AddDays(-5);
            var deletedAt = DateTimeOffset.UtcNow.AddDays(-1);
            var userId = Guid.NewGuid();

            // Act
            var branchDto = new GetAllCostumersResult.CostumerDto
            {
                Id = branchId,
                CostumerName = branchName,
                CreateAt = createAt,
                UpdateAt = updateAt,
                DeletedAt = deletedAt,
                UserId = userId
            };

            // Assert
            Assert.Equal(branchId, branchDto.Id);
            Assert.Equal(branchName, branchDto.CostumerName);
            Assert.Equal(createAt, branchDto.CreateAt);
            Assert.Equal(updateAt, branchDto.UpdateAt);
            Assert.Equal(deletedAt, branchDto.DeletedAt);
            Assert.Equal(userId, branchDto.UserId);
        }
    }
}