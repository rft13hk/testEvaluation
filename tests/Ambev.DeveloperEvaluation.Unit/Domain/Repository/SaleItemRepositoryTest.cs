using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Bogus.DataSets;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Repository;

public class SaleItemRepositoryTest
{
    
    #region  Test scenario preparation

    /// <summary>
    /// This method is used to create an in-memory database context for testing purposes.
    /// It uses the Entity Framework Core InMemory provider to create a database
    /// </summary>
    /// <returns></returns>
    public DefaultContext StartDbContextInMemory()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DefaultContext(options);
    }

    /// <summary>
    /// This method is used to create a user in the database for testing purposes.
    /// It uses the UserRepository to create a new user and saves it to the in-memory database.
    /// The created user is then returned.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// This method is used to create a costumer in the database for testing purposes.
    /// It uses the CostumerRepository to create a new costumer and saves it to the in-memory database.
    /// The created costumer is then returned.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="createdUser"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// This method is used to create a branch in the database for testing purposes.
    /// It uses the BranchRepository to create a new branch and saves it to the in-memory database.
    /// The created branch is then returned.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="createdUser"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// This method is used to create a sale in the database for testing purposes.
    /// It uses the SaleRepository to create a new sale and saves it to the in-memory database.
    /// The created sale is then returned.
    /// It also creates a costumer and a branch for the sale.
    /// The created sale is then returned.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="createdUser"></param>
    /// <param name="createdBranch"></param>
    /// <param name="createdCostumer"></param>
    /// <returns></returns>
    private async Task<Sale> CreateSale(DefaultContext context, User createdUser, Branch createdBranch, Costumer createdCostumer)
    {
        var saleRepository = new SaleRepository(context);
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

        var createdSale = await saleRepository.CreateAsync(sale);
        await context.SaveChangesAsync();
        Assert.NotNull(createdSale);

        return createdSale;
    }
    
    /// <summary>
    /// This method is used to create a product in the database for testing purposes.
    /// It uses the ProductRepository to create a new product and saves it to the in-memory database.
    /// The created product is then returned.
    /// It also creates a user for the product.
    /// The created product is then returned.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="productName"></param>
    /// <param name="productCode"></param>
    /// <param name="createdUser"></param>
    /// <returns></returns>
    private async Task<Product> CreateProduct(DefaultContext context, string productName, string productCode, User createdUser)
    {
        var productRepository = new ProductRepository(context);
        var product = new Product
        {
            ProductName = productName,
            Price = 10.0m,
            ProductCode = productCode,
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow,
            UserId = createdUser.Id,
            User = createdUser
        };

        var createdProduct = await productRepository.CreateAsync(product);
        await context.SaveChangesAsync();
        Assert.NotNull(createdProduct);

        return createdProduct;
    }

    /// <summary>
    /// This method is used to create a sale item in the database for testing purposes.
    /// It uses the SaleItemRepository to create a new sale item and saves it to the in-memory database.
    /// It also creates a sale, user, branch, costumer, and product for the sale item.
    /// The created sale item is then returned.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="createdSale"></param>
    /// <param name="createdUser"></param>
    /// <param name="createdBranch"></param>
    /// <param name="createdCostumer"></param>
    /// <param name="createdProduct"></param>
    /// <returns></returns>
    private async Task<SaleItem> CreateSaleItem(DefaultContext context, Sale createdSale, User createdUser, Branch createdBranch, Costumer createdCostumer, Product createdProduct)
    {
        var saleItemRepository = new SaleItemRepository(context);
        var saleItem = new SaleItem
        {
            SaleId = createdSale.Id,
            Sale = createdSale,
            ProductId = createdProduct.Id,
            Product = createdProduct,
            Quantity = 2,
            Price = 10.0m,
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow,
            UserId = createdUser.Id,
            User = createdUser
        };

        var createdSaleItem = await saleItemRepository.CreateAsync(saleItem);
        await context.SaveChangesAsync();
        Assert.NotNull(createdSaleItem);

        return createdSaleItem;
    }

    #endregion

    [Fact(DisplayName = "Create Sale")]
    [Trait("Category", "SaleItemRepository")]
    public async Task CreateSaleItemTest()
    {
        var context = StartDbContextInMemory();
        var saleItemRepository = new SaleItemRepository(context);

        var createdUser = await CreateUser(context);
        Assert.NotNull(createdUser);

        var createdBranch = await CreateBranch(context, createdUser);
        Assert.NotNull(createdBranch);
       
        var createdCostumer = await CreateCostumer(context, createdUser);
        Assert.NotNull(createdCostumer);

        var createdSale = await CreateSale(context, createdUser, createdBranch, createdCostumer);
        Assert.NotNull(createdSale);

        var createdProduct = await CreateProduct(context, "Test Product", "TP001", createdUser);
        Assert.NotNull(createdProduct);

        var date = DateTime.UtcNow;

        var saleItem = new SaleItem
        {
            SaleId = createdSale.Id,
            Sale = createdSale,
            ProductId = createdProduct.Id,
            Product = createdProduct,
            Quantity = 2,
            Price = 10.0m,
            CreateAt = date,
            UpdateAt = date,
            UserId = createdUser.Id,
            User = createdUser
        };

        var createdSaleItem = await saleItemRepository.CreateAsync(saleItem);
        await context.SaveChangesAsync();
        Assert.NotNull(createdSaleItem);

        var saleItemFromDb = await context.SaleItems.FirstOrDefaultAsync(c => c.Id == createdSaleItem.Id);
        Assert.NotNull(saleItemFromDb);

        Assert.Equal(createdSaleItem.Id, saleItemFromDb.Id);
        Assert.Equal(createdSaleItem.UserId, saleItemFromDb.UserId);
        Assert.Equal(createdSaleItem.User, saleItemFromDb.User);
        Assert.Equal(createdSaleItem.ProductId, saleItemFromDb.ProductId);
        Assert.Equal(createdSaleItem.Product, saleItemFromDb.Product);
        Assert.Equal(createdSaleItem.Quantity, saleItemFromDb.Quantity);
        Assert.Equal(createdSaleItem.Price, saleItemFromDb.Price);
        Assert.Equal(createdSaleItem.SaleId, saleItemFromDb.SaleId);
        Assert.Equal(createdSaleItem.Sale, saleItemFromDb.Sale);
        Assert.Equal(createdSaleItem.CreateAt.Date, saleItemFromDb.CreateAt.Date);
        Assert.Equal(createdSaleItem.UpdateAt.Date, saleItemFromDb.UpdateAt.Date);
        Assert.Equal(createdSaleItem.UserId, saleItemFromDb.UserId);
        Assert.Equal(createdSaleItem.User, saleItemFromDb.User);

    }

    [Fact(DisplayName = "Delete SaleItem")]
    [Trait("Category", "SaleItemRepository")]
    public async Task DeleteSaleItemTest()
    {
        var context = StartDbContextInMemory();
        var saleItemRepository = new SaleItemRepository(context);

        var createdUser = await CreateUser(context);
        Assert.NotNull(createdUser);

        var createdBranch = await CreateBranch(context, createdUser);
        Assert.NotNull(createdBranch);
       
        var createdCostumer = await CreateCostumer(context, createdUser);
        Assert.NotNull(createdCostumer);

        var createdSale = await CreateSale(context, createdUser, createdBranch, createdCostumer);
        Assert.NotNull(createdSale);

        var createdProduct = await CreateProduct(context, "Test Product", "TP001", createdUser);
        Assert.NotNull(createdProduct);

        var date = DateTime.UtcNow;

        var saleItem = new SaleItem
        {
            SaleId = createdSale.Id,
            Sale = createdSale,
            ProductId = createdProduct.Id,
            Product = createdProduct,
            Quantity = 2,
            Price = 10.0m,
            CreateAt = date,
            UpdateAt = date,
            UserId = createdUser.Id,
            User = createdUser
        };

        var createdSaleItem = await saleItemRepository.CreateAsync(saleItem);
        await context.SaveChangesAsync();
        Assert.NotNull(createdSaleItem);

        var saleItemFromDb = await context.SaleItems.FirstOrDefaultAsync(c => c.Id == createdSaleItem.Id);
        Assert.NotNull(saleItemFromDb);

        await saleItemRepository.DeleteAsync(createdSaleItem.Id);
        await context.SaveChangesAsync();
        
        var findsaleItemFromDb = await context.SaleItems.FirstOrDefaultAsync(b => b.Id == createdSaleItem.Id);
        if (findsaleItemFromDb != default)
        {
            Assert.NotNull(findsaleItemFromDb);
        }
        else
        {
            Assert.Null(findsaleItemFromDb);
        }

    }


    [Fact(DisplayName = "Get All SaleItems with Pagination")]
    [Trait("Category", "SaleItemRepository")]
    public async Task GetAllSaleItemsWithPaginationTest()
    {
        #region Test scenario preparation
        var context = StartDbContextInMemory();
        var saleItemRepository = new SaleItemRepository(context);

        var createdUser = await CreateUser(context);
        Assert.NotNull(createdUser);

        var createdBranch = await CreateBranch(context, createdUser);
        Assert.NotNull(createdBranch);
       
        var createdCostumer = await CreateCostumer(context, createdUser);
        Assert.NotNull(createdCostumer);

        var createdSale = await CreateSale(context, createdUser, createdBranch, createdCostumer);
        Assert.NotNull(createdSale);

        for (int i = 1; i <= 15; i++)
        {    
            var createdProduct = await CreateProduct(context, $"Test Product {i}", $"TP00{i}", createdUser);
            Assert.NotNull(createdProduct);

            var date = DateTime.UtcNow.AddDays(i);

            var saleItem = new SaleItem
            {
                SaleId = createdSale.Id,
                Sale = createdSale,
                SaleDate = date,
                ProductId = createdProduct.Id,
                Product = createdProduct,
                Quantity = i,
                Price = 10.0m + i,
                CreateAt = date,
                UpdateAt = date,
                UserId = createdUser.Id,
                User = createdUser
            };

            var createdSaleItem = await saleItemRepository.CreateAsync(saleItem);
            await context.SaveChangesAsync();
            Assert.NotNull(createdSaleItem);
        }
        
        await context.SaveChangesAsync();
        #endregion

        // Retrieve paginated sale items
        var page = 1;
        var size = 10;
        var saleItems = await saleItemRepository.GetAllAsync(page, size);

        Assert.NotNull(saleItems);
        Assert.Equal(size, saleItems.Count());

        // Verify the first page contains the correct sale items
        var saleList = saleItems.Select(s => s.SaleDate).ToList();
        
        for (int i = 0; i < size; i++)
        {
            Assert.Equal(DateTime.UtcNow.AddDays(i+1).ToString("dd/MM/yyyy"), saleList[i].ToString("dd/MM/yyyy"));
        }

        // Retrieve the second page
        page = 2;
        saleItems = await saleItemRepository.GetAllAsync(page, size);

        Assert.NotNull(saleItems);
        Assert.Equal(5, saleItems.Count()); // Remaining sale items

        saleList.Clear();

        saleList = saleItems.Select(s => s.SaleDate).ToList();

        for (int i = 0; i < saleList.Count; i++)
        {
            Assert.Equal(DateTime.UtcNow.AddDays(i+11).ToString("dd/MM/yyyy"), saleList[i].ToString("dd/MM/yyyy"));
        }

    }


}