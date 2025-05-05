using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;
/// <summary>
/// Repository interface for managing Sales
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Creates a new Sale in the repository
    /// </summary>
    /// <param name="Sale">The Sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>    
    /// <returns>The created Sale</returns>
    Task<Sale> CreateAsync(Sale Sale, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Sale by its SaleNumber
    /// </summary>
    /// <param name="SaleNumber">The SaleNumber of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    Task<Sale?> GetBySaleNumberAsync(string SaleNumber, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a paginated list of Sales based on the provided parameters
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="size">Number of items per page (default: 10)</param>
    /// <param name="order">Ordering of results (e.g., "SaleName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
    /// <returns>A list of Sales for the requested page</returns>
    Task<IEnumerable<Sale>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a Sale from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Sale to delete</param>
    /// <param name="cancellationToken"></param>
    /// <returns>True if the Sale was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves the total number of Sales in the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The total count of Sales</returns>
    Task<int> GetTotalSalesCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves pagination information for Sales
    /// </summary>
    /// <param name="pageSize">The size of each page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of Sales and total pages</returns>
    Task<(int totalSales, int totalPages)> GetSalesPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);
}
