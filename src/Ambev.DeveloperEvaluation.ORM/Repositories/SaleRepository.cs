using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Sale in the database
    /// </summary>
    /// <param name="Sale">The Sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sale</returns>
    public async Task<Sale> CreateAsync(Sale Sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(Sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Sale;
    }

    /// <summary>
    /// Retrieves a Sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .FindAsync(id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a Sale by its SaleNumber
    /// </summary>
    /// <param name="SaleNumber">The SaleNumber of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    public async Task<Sale?> GetBySaleNumberAsync(string SaleNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .FirstOrDefaultAsync(c => c.SaleNumber == SaleNumber, cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of Sales based on the provided parameters
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="size">Number of items per page (default: 10)</param>
    /// <param name="order">Ordering of results (e.g., "SaleName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
    /// <returns>A list of Sales for the requested page</returns>
    public async Task<IEnumerable<Sale>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Sale> query = _context.Sales
            .AsQueryable();

        if (activeRecordsOnly)
        {
            query = query.Where(b => b.DeletedAt == null); // Assuming there is a DeletedAt property in Sale
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
            query = query.OrderBy(b => b.SaleNumber); // Default ordering by SaleNumber ascending
        }

        return await query
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
    private IQueryable<Sale> ApplyOrdering(IQueryable<Sale> query, string propertyName, string direction)
    {
        switch (propertyName.ToLower())
        {
            case "salename":
                query = direction == "descending" ? query.OrderByDescending(b => b.SaleNumber) : query.OrderBy(b => b.SaleNumber);
                break;
            case "createat":
                query = direction == "descending" ? query.OrderByDescending(b => b.CreateAt) : query.OrderBy(b => b.CreateAt);
                break;
            case "updateat":
                query = direction == "descending" ? query.OrderByDescending(b => b.UpdateAt) : query.OrderBy(b => b.UpdateAt);
                break;
            // Add more cases for other properties you might want to order by
            default:
                query = query.OrderBy(b => b.SaleNumber); // Default fallback
                break;
        }
        return query;
    }

    /// <summary>
    /// Deletes a Sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Sale = await GetByIdAsync(id, cancellationToken);
        if (Sale == null)
            return false;

        if (Sale.DeletedAt != null)
            return false;

        try
        {
            _context.Sales.Remove(Sale);
        }
        catch
        {
            Sale.DeletedAt = DateTime.UtcNow;
            _context.Sales.Update(Sale);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }


    /// <summary>
    /// Retrieves the total count of Sales in the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>The total count of Sales</returns>
    public async Task<int> GetTotalSalesCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        if (activeRecordsOnly)
        {
            return await _context.Sales
                .Where(b => b.DeletedAt == null) // Assuming there is a DeletedAt property in Sale
                .CountAsync(cancellationToken);
        }
        // If not filtering by active records, return the total count of Sales
        return await _context.Sales.CountAsync(cancellationToken);

    }

    /// <summary>
    /// Retrieves pagination information for Sales
    /// </summary>
    /// <param name="pageSize">The size of each page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of Sales and total pages</returns>
    public async Task<(int totalSales, int totalPages)> GetSalesPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        var totalSales = await GetTotalSalesCountAsync(activeRecordsOnly, cancellationToken);
        var totalPages = (int)Math.Ceiling((double)totalSales / pageSize);
        return (totalSales, totalPages);
    }
} 

