using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Repository;

public class BranchRepositoryTest
{
    public DefaultContext StartDbContextInMemory()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: "BancoDeTeste")
            .Options;

        return new DefaultContext(options);
    }

    [Fact(DisplayName = "Create Branch")]
    [Trait("Category", "BranchRepository")]
    public async Task CreateBranchTest()
    {
        var context = StartDbContextInMemory();
        var branchRepository = new BranchRepository(context);

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

        var branch = new Branch
        {
            BranchName = "Test Branch",
            UserId = createdUser.Id,
            User = createdUser
        };

        var createdBranch = await branchRepository.CreateAsync(branch);
        await context.SaveChangesAsync();
        Assert.NotNull(createdBranch);

        var date = DateTime.UtcNow;

        var branchFromDb = await context.Branches.FirstOrDefaultAsync(b => b.Id == createdBranch.Id);
        Assert.NotNull(branchFromDb);

        Assert.Equal(branch.Id, branchFromDb.Id);
        Assert.Equal(branch.BranchName, branchFromDb.BranchName);
        Assert.Equal(branch.UserId, branchFromDb.UserId);
        Assert.Equal(branch.User, branchFromDb.User);

        Assert.Equal(date.Date, branchFromDb.CreateAt.Date);
        Assert.Equal(date.Date, branchFromDb.UpdateAt.Date);
        
        Assert.Equal(1, context.Branches.Count());
    }

    [Fact(DisplayName = "Delete Branch")]
    [Trait("Category", "BranchRepository")]
    public async Task DeleteBranchTest()
    {
    var context = StartDbContextInMemory();
        var branchRepository = new BranchRepository(context);

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

        var branch = new Branch
        {
            BranchName = "Test Branch",
            UserId = createdUser.Id,
            User = createdUser
        };

        var createdBranch = await branchRepository.CreateAsync(branch);
        await context.SaveChangesAsync();
        Assert.NotNull(createdBranch);

        await branchRepository.DeleteAsync(createdBranch.Id);
        await context.SaveChangesAsync();
        
        var branchFromDb = await context.Branches.FirstOrDefaultAsync(b => b.Id == createdBranch.Id);
        if (branchFromDb != default)
        {
            Assert.NotNull(branchFromDb);
        }
        else
        {
            Assert.Null(branchFromDb);
        }
    }

    [Fact(DisplayName = "Update Branch")]
    [Trait("Category", "BranchRepository")]
    public async Task UpdateBranchTest()
    {
        var context = StartDbContextInMemory();
        var branchRepository = new BranchRepository(context);

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

        var branch = new Branch
        {
            BranchName = "Original Branch",
            UserId = createdUser.Id,
            User = createdUser
        };

        var createdBranch = await branchRepository.CreateAsync(branch);
        await context.SaveChangesAsync();
        Assert.NotNull(createdBranch);

        // Update branch details
        createdBranch.BranchName = "Updated Branch";

        var updatedBranch = await branchRepository.UpdateAsync(createdBranch);
        await context.SaveChangesAsync();
        Assert.NotNull(updatedBranch);

        var branchFromDb = await context.Branches.FirstOrDefaultAsync(b => b.Id == createdBranch.Id);
        Assert.NotNull(branchFromDb);

        Assert.Equal("Updated Branch", branchFromDb.BranchName);
        Assert.Equal(createdBranch.Id, branchFromDb.Id);
        Assert.Equal(createdBranch.UserId, branchFromDb.UserId);
        Assert.Equal(createdBranch.User, branchFromDb.User);
    }

    [Fact(DisplayName = "Get All Branches with Pagination")]
    [Trait("Category", "BranchRepository")]
    public async Task GetAllBranchesWithPaginationTest()
    {
        var context = StartDbContextInMemory();
        var branchRepository = new BranchRepository(context);

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

        // Create multiple branches
        for (int i = 1; i <= 15; i++)
        {
            var branch = new Branch
            {
                BranchName = $"Branch {i}",
                UserId = createdUser.Id,
                User = createdUser
            };

            listToCompare.Add(branch.BranchName);

            await branchRepository.CreateAsync(branch);
        }
        await context.SaveChangesAsync();

        var listToCompareOrdered = listToCompare.OrderBy(o => o).ToList();    

        // Retrieve paginated branches
        var page = 1;
        var size = 10;
        var branches = await branchRepository.GetAllAsync(page, size);

        Assert.NotNull(branches);
        Assert.Equal(size, branches.Count());

        // Verify the first page contains the correct branches
        var branchList = branches.OrderBy(o => o.BranchName).ToList();
        for (int i = 0; i < size; i++)
        {
            Assert.Equal(listToCompareOrdered[i], branchList[i].BranchName);
        }

        // Retrieve the second page
        page = 2;
        branches = await branchRepository.GetAllAsync(page, size);

        Assert.NotNull(branches);
        Assert.Equal(5, branches.Count()); // Remaining branches

        branchList = branches.OrderBy(o => o.BranchName).ToList();
        for (int i = 0; i < branchList.Count; i++)
        {
            Assert.Equal(listToCompareOrdered[i+10], branchList[i].BranchName);
        }
    }


}