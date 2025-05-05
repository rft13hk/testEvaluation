using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Product in the database
    /// </summary>
    /// <param name="Product">The Product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Product</returns>
    public async Task<Product> CreateAsync(Product Product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(Product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Product;
    }

    /// <summary>
    /// Updates an existing Product in the database
    /// </summary>
    /// <param name="product">The Product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Product</returns>
    public async Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);
        if (existingProduct == null || existingProduct.DeletedAt != null)
            return null;

        _context.Entry(existingProduct).CurrentValues.SetValues(product);
        existingProduct.UpdateAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return existingProduct;
    }


    /// <summary>
    /// Retrieves a Product by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(b => b.User) // Assuming there is a navigation property for User in Product
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken); // Changed to FirstOrDefaultAsync for better null handling
    }

    /// <summary>
    /// Retrieves a Product by its CPF
    /// </summary>
    /// <param name="cpf">The CPF of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    public async Task<Product?> GetByProductCodeAsync(string productCode, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(c => c.User) // Assuming there is a navigation property for User in Product
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .FirstOrDefaultAsync(c => c.DeletedAt == null && c.ProductCode == productCode, cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of Products based on the provided parameters
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="size">Number of items per page (default: 10)</param>
    /// <param name="order">Ordering of results (e.g., "ProductName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
    /// <returns>A list of Products for the requested page</returns>
    public async Task<IEnumerable<Product>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = _context.Products
            .AsQueryable();

        if (activeRecordsOnly)
        {
            query = query.Where(b => b.DeletedAt == null); // Assuming there is a DeletedAt property in Product
        }

        if (!string.IsNullOrEmpty(order))
        {
            var orderParams = order.Split(',', StringSplitOptions.TrimEntries);
            foreach (var param in orderParams)
            {
                var parts = param.Split(' ', StringSplitOptions.TrimEntries);
                var propertyName = parts[0];
                var direction = parts.Length > 1 && parts[1].ToLower() == "desc" ? "descending" : "ascending";

                query = ApplyOrdering(query, propertyName, direction);
            }
        }
        else
        {
            query = query.OrderBy(b => b.ProductName); // Default ordering by ProductName ascending
        }

        return await query
            .Include(b => b.User) // Assuming there is a navigation property for User in Product
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Helper method to apply ordering dynamically
    /// </summary>
    /// <param name="query">The IQueryable to order</param>
    /// <param name="propertyName">The name of the property to order by</param>
    /// <param name="direction">The ordering direction ("ascending" or "descending")</param>
    /// <returns>The ordered IQueryable</returns>
    private IQueryable<Product> ApplyOrdering(IQueryable<Product> query, string propertyName, string direction)
    {
        switch (propertyName.ToLower())
        {
            case "Productname":
                query = direction == "descending" ? query.OrderByDescending(b => b.ProductName) : query.OrderBy(b => b.ProductName);
                break;
            case "createat":
                query = direction == "descending" ? query.OrderByDescending(b => b.CreateAt) : query.OrderBy(b => b.CreateAt);
                break;
            case "updateat":
                query = direction == "descending" ? query.OrderByDescending(b => b.UpdateAt) : query.OrderBy(b => b.UpdateAt);
                break;
            // Add more cases for other properties you might want to order by
            default:
                query = query.OrderBy(b => b.ProductName); // Default fallback
                break;
        }
        return query;
    }

    /// <summary>
    /// Deletes a Product from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Product was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Product = await GetByIdAsync(id, cancellationToken);
        if (Product == null)
            return false;

        if (Product.DeletedAt != null)
            return false;

        try
        {
            _context.Products.Remove(Product);
        }
        catch
        {
            Product.DeletedAt = DateTime.UtcNow;
            _context.Products.Update(Product);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }


    /// <summary>
    /// Retrieves the total count of Products in the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>The total count of Products</returns>
    public async Task<int> GetTotalProductsCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        if (activeRecordsOnly)
        {
            return await _context.Products
                .Where(b => b.DeletedAt == null) // Assuming there is a DeletedAt property in Product
                .CountAsync(cancellationToken);
        }
        // If not filtering by active records, return the total count of Products
        return await _context.Products.CountAsync(cancellationToken);

    }

    /// <summary>
    /// Retrieves pagination information for Products
    /// </summary>
    /// <param name="pageSize">The size of each page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of Products and total pages</returns>
    public async Task<(int totalProducts, int totalPages)> GetProductsPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        // Validate pageSize
        if (pageSize <= 0)
            throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));
            
        // Get the total count of Products
        var totalProducts = await GetTotalProductsCountAsync(activeRecordsOnly, cancellationToken);

        // If there are no Products, return 0 for both totalProducts and totalPages
        // This is important to avoid division by zero in the next step
        // and to ensure that the pagination logic works correctly
        // e.g., if totalProducts = 0 and pageSize = 10, totalPages = 0
        if (totalProducts == 0)
            return (0, 0);

        // Calculate total pages based on the total number of Products and the page size
        var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

        return (totalProducts, totalPages);
    }
} 

