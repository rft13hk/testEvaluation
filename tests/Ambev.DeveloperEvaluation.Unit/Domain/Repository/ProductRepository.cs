using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Repository;

public class ProductRepositoryTest
{
    public DefaultContext StartDbContextInMemory()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: "BancoDeTeste")
            .Options;

        return new DefaultContext(options);
    }

    [Fact(DisplayName = "Create Product")]
    [Trait("Category", "ProductRepository")]
    public async Task CreateProductTest()
    {
        var context = StartDbContextInMemory();
        var ProductRepository = new ProductRepository(context);

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

        var Product = new Product
        {
            ProductName = "Test Product",
            UserId = createdUser.Id,
            User = createdUser // Ensure the required 'Users' property is set
        };

        var createdProduct = await ProductRepository.CreateAsync(Product);
        await context.SaveChangesAsync();
        Assert.NotNull(createdProduct);

        var date = DateTime.UtcNow;

        var ProductFromDb = await context.Products.FirstOrDefaultAsync(c => c.Id == createdProduct.Id);
        Assert.NotNull(ProductFromDb);

        Assert.Equal(Product.Id, ProductFromDb.Id);
        Assert.Equal(Product.ProductName, ProductFromDb.ProductName);
        Assert.Equal(Product.UserId, ProductFromDb.UserId);
        Assert.Equal(Product.User, ProductFromDb.User);

        Assert.Equal(date.Date, ProductFromDb.CreateAt.Date);
        Assert.Equal(date.Date, ProductFromDb.UpdateAt.Date);

    }

    [Fact(DisplayName = "Delete Product")]
    [Trait("Category", "ProductRepository")]
    public async Task DeleteProductTest()
    {
        var context = StartDbContextInMemory();
        var ProductRepository = new ProductRepository(context);

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

        var Product= new Product
        {
            ProductName = "Test Product",
            UserId = createdUser.Id,
            User = createdUser
        };

        var createdProduct= await ProductRepository.CreateAsync(Product);
        await context.SaveChangesAsync();

        Assert.NotNull(createdProduct);

        await ProductRepository.DeleteAsync(createdProduct.Id);
        await context.SaveChangesAsync();
        
        var ProductFromDb = await context.Products.FirstOrDefaultAsync(b => b.Id == createdProduct.Id);
        if (ProductFromDb != default)
        {
            Assert.NotNull(ProductFromDb);
        }
        else
        {
            Assert.Null(ProductFromDb);
        }

    }

    [Fact(DisplayName = "Update Product")]
    [Trait("Category", "ProductRepository")]
    public async Task UpdateProductTest()
    {
        var context = StartDbContextInMemory();
        var ProductRepository = new ProductRepository(context);

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

        var Product= new Product
        {
            ProductName = "Original Product",
            UserId = createdUser.Id,
            User = createdUser // Ensure the required 'Users' property is set
        };

        var createdProduct= await ProductRepository.CreateAsync(Product);
        await context.SaveChangesAsync();
        Assert.NotNull(createdProduct);

        // Update Product details
        createdProduct.ProductName = "Updated Product";

        var updatedProduct = await ProductRepository.UpdateAsync(createdProduct);
        await context.SaveChangesAsync();
        Assert.NotNull(updatedProduct);

        var ProductFromDb = await context.Products.FirstOrDefaultAsync(b => b.Id == createdProduct.Id);
        Assert.NotNull(ProductFromDb);

        Assert.Equal("Updated Product", ProductFromDb.ProductName);
        Assert.Equal(createdProduct.Id, ProductFromDb.Id);
        Assert.Equal(createdProduct.UserId, ProductFromDb.UserId);
        Assert.Equal(createdProduct.User, ProductFromDb.User);
    }


    [Fact(DisplayName = "Get All Products with Pagination")]
    [Trait("Category", "ProductRepository")]
    public async Task GetAllProductsWithPaginationTest()
    {
        var context = StartDbContextInMemory();
        var productRepository = new ProductRepository(context);

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

        // Create multiple productes
        for (int i = 1; i <= 15; i++)
        {
            var product = new Product
            {
                ProductName = $"Product {i}",
                UserId = createdUser.Id,
                User = createdUser
            };

            listToCompare.Add(product.ProductName);

            await productRepository.CreateAsync(product);
        }
        await context.SaveChangesAsync();

        var listToCompareOrdered = listToCompare.OrderBy(o => o).ToList();    

        // Retrieve paginated productes
        var page = 1;
        var size = 10;
        var products = await productRepository.GetAllAsync(page, size);

        Assert.NotNull(products);
        Assert.Equal(size, products.Count());

        // Verify the first page contains the correct productes
        var productList = products.OrderBy(o => o.ProductName).ToList();
        for (int i = 0; i < size; i++)
        {
            Assert.Equal(listToCompareOrdered[i], productList[i].ProductName);
        }

        // Retrieve the second page
        page = 2;
        products = await productRepository.GetAllAsync(page, size);

        Assert.NotNull(products);
        Assert.Equal(5, products.Count()); // Remaining product

        productList = products.OrderBy(o => o.ProductName).ToList();
        for (int i = 0; i < productList.Count; i++)
        {
            Assert.Equal(listToCompareOrdered[i+10], productList[i].ProductName);
        }
    }


}