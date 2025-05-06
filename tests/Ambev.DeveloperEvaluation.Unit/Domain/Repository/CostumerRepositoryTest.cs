using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Repository;

public class CostumerRepositoryTest
{
    public DefaultContext StartDbContextInMemory()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name for each instance
            .Options;

        return new DefaultContext(options);
    }

    [Fact(DisplayName = "Create Costumer")]
    [Trait("Category", "CostumerRepository")]
    public async Task CreateCostumerTest()
    {
        var context = StartDbContextInMemory();
        var costumerRepository = new CostumerRepository(context);

        #region Create User
        var userRepository = new UserRepository(context);

        var user = new Ambev.DeveloperEvaluation.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Phone = "1234567890",
            Role = UserRole.None,
            Status = UserStatus.Active,
            Email = "testuser@example.com",
            Password = "password",
        };

        var createdUser = await userRepository.CreateAsync(user);
        await context.SaveChangesAsync();
        Assert.NotNull(createdUser);

        #endregion

        var costumer = new Costumer
        {
            CostumerName = "Test Costumer",
            UserId = createdUser.Id,
            Users = createdUser // Ensure the required 'Users' property is set
        };

        var createdCostumer = await costumerRepository.CreateAsync(costumer);
        await context.SaveChangesAsync();
        Assert.NotNull(createdCostumer);

        var date = DateTime.UtcNow;

        var costumerFromDb = await context.Costumers.FirstOrDefaultAsync(c => c.Id == createdCostumer.Id);
        Assert.NotNull(costumerFromDb);

        Assert.Equal(costumer.Id, costumerFromDb.Id);
        Assert.Equal(costumer.CostumerName, costumerFromDb.CostumerName);
        Assert.Equal(costumer.UserId, costumerFromDb.UserId);
        Assert.Equal(costumer.Users, costumerFromDb.Users);

        Assert.Equal(date.Date, costumerFromDb.CreateAt.Date);
        Assert.Equal(date.Date, costumerFromDb.UpdateAt.Date);

    }

    [Fact(DisplayName = "Delete Costumer")]
    [Trait("Category", "CostumerRepository")]
    public async Task DeleteCostumerTest()
    {
        var context = StartDbContextInMemory();
        var costumerRepository = new CostumerRepository(context);

        #region Create User
        var userRepository = new UserRepository(context);

        var user = new Ambev.DeveloperEvaluation.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Phone = "1234567890",
            Role = UserRole.None,
            Status = UserStatus.Active,
            Email = "testuser@example.com",
            Password = "password",
        };

        var createdUser = await userRepository.CreateAsync(user);
        await context.SaveChangesAsync();
        Assert.NotNull(createdUser);

        #endregion

        var costumer= new Costumer
        {
            CostumerName = "Test Costumer",
            UserId = createdUser.Id,
            Users = createdUser
        };

        var createdCostumer= await costumerRepository.CreateAsync(costumer);
        await context.SaveChangesAsync();

        Assert.NotNull(createdCostumer);

        await costumerRepository.DeleteAsync(createdCostumer.Id);
        await context.SaveChangesAsync();
        
        var costumerFromDb = await context.Costumers.FirstOrDefaultAsync(b => b.Id == createdCostumer.Id);
        if (costumerFromDb != default)
        {
            Assert.NotNull(costumerFromDb);
        }
        else
        {
            Assert.Null(costumerFromDb);
        }

    }

    [Fact(DisplayName = "Update Costumer")]
    [Trait("Category", "CostumerRepository")]
    public async Task UpdateCostumerTest()
    {
        var context = StartDbContextInMemory();
        var costumerRepository = new CostumerRepository(context);

        #region Create User
        var userRepository = new UserRepository(context);

        var user = new Ambev.DeveloperEvaluation.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Phone = "1234567890",
            Role = UserRole.None,
            Status = UserStatus.Active,
            Email = "testuser@example.com",
            Password = "password",
        };

        var createdUser = await userRepository.CreateAsync(user);
        await context.SaveChangesAsync();
        Assert.NotNull(createdUser);

        #endregion

        var costumer= new Costumer
        {
            CostumerName = "Original Costumer",
            UserId = createdUser.Id,
            Users = createdUser // Ensure the required 'Users' property is set
        };

        var createdCostumer= await costumerRepository.CreateAsync(costumer);
        await context.SaveChangesAsync();
        Assert.NotNull(createdCostumer);

        // Update costumer details
        createdCostumer.CostumerName = "Updated Costumer";

        var updatedCostumer = await costumerRepository.UpdateAsync(createdCostumer);
        await context.SaveChangesAsync();
        Assert.NotNull(updatedCostumer);

        var costumerFromDb = await context.Costumers.FirstOrDefaultAsync(b => b.Id == createdCostumer.Id);
        Assert.NotNull(costumerFromDb);

        Assert.Equal("Updated Costumer", costumerFromDb.CostumerName);
        Assert.Equal(createdCostumer.Id, costumerFromDb.Id);
        Assert.Equal(createdCostumer.UserId, costumerFromDb.UserId);
        Assert.Equal(createdCostumer.Users, costumerFromDb.Users);
    }

    [Fact(DisplayName = "Get All Costumeres with Pagination")]
    [Trait("Category", "CostumerRepository")]
    public async Task GetAllCostumeresWithPaginationTest()
    {
        var context = StartDbContextInMemory();
        var costumerRepository = new CostumerRepository(context);

        #region Create User
        var userRepository = new UserRepository(context);

        var user = new Ambev.DeveloperEvaluation.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Phone = "1234567890",
            Role = UserRole.None,
            Status = UserStatus.Active,
            Email = "testuser@example.com",
            Password = "password",
        };

        var createdUser = await userRepository.CreateAsync(user);
        await context.SaveChangesAsync();
        Assert.NotNull(createdUser);

        #endregion

        var listToCompare = new List<string>();

        // Create multiple costumeres
        for (int i = 1; i <= 15; i++)
        {
            var costumer = new Costumer
            {
                CostumerName = $"Costumer {i}",
                UserId = createdUser.Id,
                Users = createdUser
            };

            listToCompare.Add(costumer.CostumerName);

            await costumerRepository.CreateAsync(costumer);
        }
        await context.SaveChangesAsync();

        var listToCompareOrdered = listToCompare.OrderBy(o => o).ToList();    

        // Retrieve paginated costumeres
        var page = 1;
        var size = 10;
        var costumers = await costumerRepository.GetAllAsync(page, size);

        Assert.NotNull(costumers);
        Assert.Equal(size, costumers.Count());

        // Verify the first page contains the correct costumeres
        var costumerList = costumers.OrderBy(o => o.CostumerName).ToList();
        for (int i = 0; i < size; i++)
        {
            Assert.Equal(listToCompareOrdered[i], costumerList[i].CostumerName);
        }

        // Retrieve the second page
        page = 2;
        costumers = await costumerRepository.GetAllAsync(page, size);

        Assert.NotNull(costumers);
        Assert.Equal(5, costumers.Count()); // Remaining costumer

        costumerList = costumers.OrderBy(o => o.CostumerName).ToList();
        for (int i = 0; i < costumerList.Count; i++)
        {
            Assert.Equal(listToCompareOrdered[i+10], costumerList[i].CostumerName);
        }
    }


}