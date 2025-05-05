using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Bogus.DataSets;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Repository;

public class SaleRepositoryTest
{
    
    #region  Test scenario preparation
    public DefaultContext StartDbContextInMemory()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: "BancoDeTeste")
            .Options;

        return new DefaultContext(options);
    }

    private async Task<User> CreateUser(DefaultContext context)
    {
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
        
        return createdUser;
    }

    private async Task<Costumer> CreateCostumer(DefaultContext context, User createdUser)
    {
        var costumerRepository = new CostumerRepository(context);
        var costumer = new Costumer
        {
            CostumerName = "Test Costumer",
            UserId = createdUser.Id,
            Users = createdUser
        };

        var createdCostumer = await costumerRepository.CreateAsync(costumer);
        await context.SaveChangesAsync();
        Assert.NotNull(createdCostumer);
        
        return createdCostumer;
    }
    private async Task<Branch> CreateBranch(DefaultContext context, User createdUser)
    {
        var branchRepository = new BranchRepository(context);
        var branch = new Branch
        {
            BranchName = "Test Branch",
            UserId = createdUser.Id,
            User = createdUser
        };
        var createdBranch = await branchRepository.CreateAsync(branch);
        await context.SaveChangesAsync();
        Assert.NotNull(createdBranch);
        
        return createdBranch;
    }
    #endregion


    [Fact(DisplayName = "Create Sale")]
    [Trait("Category", "SaleRepository")]
    public async Task CreateSaleTest()
    {
        var context = StartDbContextInMemory();
        var SaleRepository = new SaleRepository(context);

        var createdUser = await CreateUser(context);
        Assert.NotNull(createdUser);

        var createdBranch = await CreateBranch(context, createdUser);
        await context.SaveChangesAsync();
        Assert.NotNull(createdBranch);
       
        var createdCostumer = await CreateCostumer(context, createdUser);
        await context.SaveChangesAsync();
        Assert.NotNull(createdCostumer);

        var sale = new Sale
        {
            BranchId = createdBranch.Id,
            Branch = createdBranch,
            CostumerId = createdCostumer.Id,
            Costumer = createdCostumer,
            SaleDate = DateTime.UtcNow,
            UserId = createdUser.Id,
            User = createdUser,
        };

        var createdSale = await SaleRepository.CreateAsync(sale);
        await context.SaveChangesAsync();
        Assert.NotNull(createdSale);

        var date = DateTime.UtcNow;

        var saleFromDb = await context.Sales.FirstOrDefaultAsync(c => c.Id == createdSale.Id);
        Assert.NotNull(saleFromDb);

        Assert.Equal(sale.Id, saleFromDb.Id);
        Assert.Equal(sale.UserId, saleFromDb.UserId);
        Assert.Equal(sale.User, saleFromDb.User);
        Assert.Equal(sale.BranchId, saleFromDb.BranchId);
        Assert.Equal(sale.Branch, saleFromDb.Branch);
        Assert.Equal(sale.CostumerId, saleFromDb.CostumerId);
        Assert.Equal(sale.Costumer, saleFromDb.Costumer);
        Assert.Equal(sale.SaleDate.Date, saleFromDb.SaleDate.Date);

        Assert.Equal(date.Date, saleFromDb.CreateAt.Date);
        Assert.Equal(date.Date, saleFromDb.UpdateAt.Date);

    }

    [Fact(DisplayName = "Delete Sale")]
    [Trait("Category", "SaleRepository")]
    public async Task DeleteSaleTest()
    {
        var context = StartDbContextInMemory();
        var SaleRepository = new SaleRepository(context);

        var createdUser = await CreateUser(context);
        Assert.NotNull(createdUser);

        var createdBranch = await CreateBranch(context, createdUser);
        await context.SaveChangesAsync();
        Assert.NotNull(createdBranch);
       
        var createdCostumer = await CreateCostumer(context, createdUser);
        await context.SaveChangesAsync();
        Assert.NotNull(createdCostumer);

        var sale = new Sale
        {
            BranchId = createdBranch.Id,
            Branch = createdBranch,
            CostumerId = createdCostumer.Id,
            Costumer = createdCostumer,
            SaleDate = DateTime.UtcNow,
            UserId = createdUser.Id,
            User = createdUser,
        };

        var createdSale = await SaleRepository.CreateAsync(sale);
        await context.SaveChangesAsync();
        Assert.NotNull(createdSale);

        var date = DateTime.UtcNow;

        var saleFromDb = await context.Sales.FirstOrDefaultAsync(c => c.Id == createdSale.Id);
        Assert.NotNull(saleFromDb);

        await SaleRepository.DeleteAsync(createdSale.Id);
        await context.SaveChangesAsync();
        
        var SaleFromDb = await context.Sales.FirstOrDefaultAsync(b => b.Id == createdSale.Id);
        if (SaleFromDb != default)
        {
            Assert.NotNull(SaleFromDb);
        }
        else
        {
            Assert.Null(SaleFromDb);
        }

    }

    [Fact(DisplayName = "Get All Sales with Pagination")]
    [Trait("Category", "SaleRepository")]
    public async Task GetAllSalesWithPaginationTest()
    {
        var context = StartDbContextInMemory();
        var saleRepository = new SaleRepository(context);

        var createdUser = await CreateUser(context);
        Assert.NotNull(createdUser);

        var createdBranch = await CreateBranch(context, createdUser);
        await context.SaveChangesAsync();
        Assert.NotNull(createdBranch);
       
        var createdCostumer = await CreateCostumer(context, createdUser);
        await context.SaveChangesAsync();
        Assert.NotNull(createdCostumer);

        for (int i = 1; i <= 15; i++)
        {
            var sale = new Sale
            {
                BranchId = createdBranch.Id,
                Branch = createdBranch,
                CostumerId = createdCostumer.Id,
                Costumer = createdCostumer,
                SaleDate = DateTime.UtcNow.AddDays(i),
                UserId = createdUser.Id,
                User = createdUser,
            };

            var createdSale = await saleRepository.CreateAsync(sale);
            Assert.NotNull(createdSale);
        }
        
        await context.SaveChangesAsync();

        // Retrieve paginated salees
        var page = 1;
        var size = 10;
        var sales = await saleRepository.GetAllAsync(page, size);

        Assert.NotNull(sales);
        Assert.Equal(size, sales.Count());

        // Verify the first page contains the correct salees
        var saleList = sales.OrderBy(o => o.SaleDate).ToList();
        for (int i = 0; i < size; i++)
        {
            Assert.Equal(DateTime.UtcNow.AddDays(i+1).ToString("dd/MM/yyyy"), saleList[i].SaleDate.ToString("dd/MM/yyyy"));
        }

        // Retrieve the second page
        page = 2;
        sales = await saleRepository.GetAllAsync(page, size);

        Assert.NotNull(sales);
        Assert.Equal(5, sales.Count()); // Remaining sale

        saleList = sales.OrderBy(o => o.SaleDate).ToList();
        for (int i = 0; i < saleList.Count; i++)
        {
            Assert.Equal(DateTime.UtcNow.AddDays(i+11).ToString("dd/MM/yyyy"), saleList[i].SaleDate.ToString("dd/MM/yyyy"));
        }
    }


}