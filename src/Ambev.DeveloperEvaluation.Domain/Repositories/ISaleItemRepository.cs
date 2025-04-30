using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for managing SaleItems
    /// </summary>
    public interface ISaleItemRepository
    {
        /// <summary>
        /// Creates a new SaleItem in the repository
        /// </summary>
        /// <param name="SaleItem">The SaleItem to create</param>
        /// <param name="cancellationToken">Cancellation token</param>    
        /// <returns>The created SaleItem</returns>
        Task<SaleItem> CreateAsync(SaleItem SaleItem, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a SaleItem by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the SaleItem</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The SaleItem if found, null otherwise</returns>
        Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
 
        /// <summary>
        /// Retrieves a paginated list of SaleItems based on the provided parameters
        /// </summary>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="size">Number of items per page (default: 10)</param>
        /// <param name="order">Ordering of results (e.g., "SaleItemName desc, CreateAt asc")</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
        /// <returns>A list of SaleItems for the requested page</returns>
        Task<IEnumerable<SaleItem>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);


        /// <summary>
        /// Deletes a SaleItem from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the SaleItem to delete</param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if the SaleItem was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);


        /// <summary>
        /// Retrieves the total number of SaleItems in the repository
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The total count of SaleItems</returns>
        Task<int> GetTotalSaleItemsCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves pagination information for SaleItems
        /// </summary>
        /// <param name="pageSize">The size of each page</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A tuple containing the total number of SaleItems and total pages</returns>
        Task<(int totalSaleItems, int totalPages)> GetSaleItemsPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);
    }
}