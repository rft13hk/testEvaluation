using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
/// <summary>
/// Repository interface for managing Products
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Creates a new Product in the repository
    /// </summary>
    /// <param name="Product">The Product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>    
    /// <returns>The created Product</returns>
    Task<Product> CreateAsync(Product Product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Product by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Product by its Product Code
    /// </summary>
    /// <param name="productCode">The Product Code of the Product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Product if found, null otherwise</returns>
    Task<Product?> GetByProductCodeAsync(string productCode, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a paginated list of Products based on the provided parameters
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="size">Number of items per page (default: 10)</param>
    /// <param name="order">Ordering of results (e.g., "ProductName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
    /// <returns>A list of Products for the requested page</returns>
    Task<IEnumerable<Product>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a Product from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Product to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True if the Product was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves the total number of Products in the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The total count of Products</returns>
    Task<int> GetTotalProductsCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves pagination information for Products
    /// </summary>
    /// <param name="pageSize">The size of each page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of Products and total pages</returns>
    Task<(int totalProducts, int totalPages)> GetProductsPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);
}
