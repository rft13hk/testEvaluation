using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for managing Costumers
    /// </summary>
    public interface ICostumerRepository
    {
        /// <summary>
        /// Creates a new Costumer in the repository
        /// </summary>
        /// <param name="Costumer">The Costumer to create</param>
        /// <param name="cancellationToken">Cancellation token</param>    
        /// <returns>The created Costumer</returns>
        Task<Costumer> CreateAsync(Costumer Costumer, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a Costumer by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the Costumer</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The Costumer if found, null otherwise</returns>
        Task<Costumer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
 
        /// <summary>
        /// Retrieves a Costumer by its CPF
        /// </summary>
        /// <param name="cpf">The CPF of the Costumer</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The Costumer if found, null otherwise</returns>
        Task<Costumer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default);


        /// <summary>
        /// Retrieves a paginated list of Costumers based on the provided parameters
        /// </summary>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="size">Number of items per page (default: 10)</param>
        /// <param name="order">Ordering of results (e.g., "CostumerName desc, CreateAt asc")</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
        /// <returns>A list of Costumers for the requested page</returns>
        Task<IEnumerable<Costumer>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);


        /// <summary>
        /// Deletes a Costumer from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the Costumer to delete</param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if the Costumer was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);


        /// <summary>
        /// Retrieves the total number of Costumers in the repository
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The total count of Costumers</returns>
        Task<int> GetTotalCostumersCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves pagination information for Costumers
        /// </summary>
        /// <param name="pageSize">The size of each page</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A tuple containing the total number of Costumers and total pages</returns>
        Task<(int totalCostumers, int totalPages)> GetCostumersPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default);
    }
}