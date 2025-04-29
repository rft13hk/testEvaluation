using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for managing branches
    /// </summary>
    public interface IBranchRepository
    {
        /// <summary>
        /// Creates a new branch in the repository
        /// </summary>
        /// <param name="branch">The branch to create</param>
        /// <param name="cancellationToken">Cancellation token</param>    
        /// <returns>The created branch</returns>
        Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a branch by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the branch</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The branch if found, null otherwise</returns>
        Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
 
        /// <summary>
        /// Retrieves a paginated list of branches based on the provided parameters
        /// </summary>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="size">Number of items per page (default: 10)</param>
        /// <param name="order">Ordering of results (e.g., "BranchName desc, CreateAt asc")</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
        /// <returns>A list of branches for the requested page</returns>
        Task<IEnumerable<Branch>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);


        /// <summary>
        /// Deletes a branch from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the branch to delete</param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if the branch was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);


        /// <summary>
        /// Retrieves the total number of branches in the repository
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The total count of branches</returns>
        Task<int> GetTotalBranchesCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves pagination information for branches
        /// </summary>
        /// <param name="pageSize">The size of each page</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A tuple containing the total number of branches and total pages</returns>
        Task<(int totalBranches, int totalPages)> GetBranchesPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);
    }
}