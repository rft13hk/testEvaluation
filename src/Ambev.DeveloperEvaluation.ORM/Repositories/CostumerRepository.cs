using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICostumerRepository using Entity Framework Core
/// </summary>
public class CostumerRepository : ICostumerRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of CostumerRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public CostumerRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Costumer in the database
    /// </summary>
    /// <param name="Costumer">The Costumer to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Costumer</returns>
    public async Task<Costumer> CreateAsync(Costumer Costumer, CancellationToken cancellationToken = default)
    {
        await _context.Costumers.AddAsync(Costumer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Costumer;
    }

    /// <summary>
    /// Retrieves a Costumer by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Costumer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Costumer if found, null otherwise</returns>
    public async Task<Costumer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Costumers
            .Include(b => b.Users) // Assuming there is a navigation property for Users in Costumer
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a Costumer by its CPF
    /// </summary>
    /// <param name="cpf">The CPF of the Costumer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Costumer if found, null otherwise</returns>
    public async Task<Costumer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken = default)
    {
        return await _context.Costumers
            .Where(c => c.DeletedAt == null) // Assuming there is a DeletedAt property in Costumer
            .Include(c => c.Users) // Assuming there is a navigation property for Users in Costumer
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .FirstOrDefaultAsync(c => c.CPF == cpf, cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of Costumers based on the provided parameters
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="size">Number of items per page (default: 10)</param>
    /// <param name="order">Ordering of results (e.g., "CostumerName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
    /// <returns>A list of Costumers for the requested page</returns>
    public async Task<IEnumerable<Costumer>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Costumer> query = _context.Costumers
            .AsQueryable();

        if (activeRecordsOnly)
        {
            query = query.Where(b => b.DeletedAt == null); // Assuming there is a DeletedAt property in Costumer
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
            query = query.OrderBy(b => b.CostumerName); // Default ordering by CostumerName ascending
        }

        return await query
            .Include(b => b.Users) // Assuming there is a navigation property for Users in Costumer
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
    private IQueryable<Costumer> ApplyOrdering(IQueryable<Costumer> query, string propertyName, string direction)
    {
        switch (propertyName.ToLower())
        {
            case "Costumername":
                query = direction == "descending" ? query.OrderByDescending(b => b.CostumerName) : query.OrderBy(b => b.CostumerName);
                break;
            case "createat":
                query = direction == "descending" ? query.OrderByDescending(b => b.CreateAt) : query.OrderBy(b => b.CreateAt);
                break;
            case "updateat":
                query = direction == "descending" ? query.OrderByDescending(b => b.UpdateAt) : query.OrderBy(b => b.UpdateAt);
                break;
            // Add more cases for other properties you might want to order by
            default:
                query = query.OrderBy(b => b.CostumerName); // Default fallback
                break;
        }
        return query;
    }

    /// <summary>
    /// Deletes a Costumer from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Costumer to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Costumer was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Costumer = await GetByIdAsync(id, cancellationToken);
        if (Costumer == null)
            return false;

        if (Costumer.DeletedAt != null)
            return false;

        try
        {
            _context.Costumers.Remove(Costumer);
        }
        catch
        {
            Costumer.DeletedAt = DateTime.UtcNow;
            _context.Costumers.Update(Costumer);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }


    /// <summary>
    /// Retrieves the total count of Costumers in the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>The total count of Costumers</returns>
    public async Task<int> GetTotalCostumersCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        if (activeRecordsOnly)
        {
            return await _context.Costumers
                .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance       
                .Where(b => b.DeletedAt == null) // Assuming there is a DeletedAt property in Costumer
                .CountAsync(cancellationToken);
        }
        // If not filtering by active records, return the total count of Costumers
        return await _context.Costumers.CountAsync(cancellationToken);

    }

    /// <summary>
    /// Retrieves pagination information for Costumers
    /// </summary>
    /// <param name="pageSize">The size of each page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of Costumers and total pages</returns>
    public async Task<(int totalCostumers, int totalPages)> GetCostumersPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        // Validate pageSize
        if (pageSize <= 0)
            throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));

        // Get the total number of Costumers
        var totalCostumers = await GetTotalCostumersCountAsync(activeRecordsOnly, cancellationToken);

        // If there are no Costumers, return 0 for both totalCostumers and totalPages
        // This is important to avoid division by zero in the next step
        // and to ensure that the pagination logic works correctly
        // e.g., if totalCostumers = 0 and pageSize = 10, totalPages = 0
        if (totalCostumers == 0)
            return (0, 0);

        // Calculate total pages
        var totalPages = (int)Math.Ceiling((double)totalCostumers / pageSize);

        return (totalCostumers, totalPages);
    }
} 

