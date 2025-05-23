using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleItemRepository using Entity Framework Core
/// </summary>
public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleItemRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleItemRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new SaleItem in the database
    /// </summary>
    /// <param name="SaleItem">The SaleItem to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created SaleItem</returns>
    public async Task<SaleItem> CreateAsync(SaleItem SaleItem, CancellationToken cancellationToken = default)
    {
        await _context.SaleItems.AddAsync(SaleItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return SaleItem;
    }

    /// <summary>
    /// Retrieves a SaleItem by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the SaleItem</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The SaleItem if found, null otherwise</returns>
    public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .FindAsync(id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of SaleItems based on the provided parameters
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="size">Number of items per page (default: 10)</param>
    /// <param name="order">Ordering of results (e.g., "SaleItemName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
    /// <returns>A list of SaleItems for the requested page</returns>
    public async Task<IEnumerable<SaleItem>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        IQueryable<SaleItem> query = _context.SaleItems
            .AsQueryable();

        if (activeRecordsOnly)
        {
            query = query.Where(b => b.DeletedAt == null); // Assuming there is a DeletedAt property in SaleItem
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
            query = query.OrderBy(b => b.SaleDate); // Default ordering by SaleDate ascending
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
    private IQueryable<SaleItem> ApplyOrdering(IQueryable<SaleItem> query, string propertyName, string direction)
    {
        switch (propertyName.ToLower())
        {
            case "saleitemname":
                query = direction == "descending" ? query.OrderByDescending(b => b.SaleDate) : query.OrderBy(b => b.SaleDate);
                break;
            case "createat":
                query = direction == "descending" ? query.OrderByDescending(b => b.CreateAt) : query.OrderBy(b => b.CreateAt);
                break;
            case "updateat":
                query = direction == "descending" ? query.OrderByDescending(b => b.UpdateAt) : query.OrderBy(b => b.UpdateAt);
                break;
            // Add more cases for other properties you might want to order by
            default:
                query = query.OrderBy(b => b.SaleDate); // Default fallback
                break;
        }
        return query;
    }

    /// <summary>
    /// Deletes a SaleItem from the database
    /// </summary>
    /// <param name="id">The unique identifier of the SaleItem to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the SaleItem was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var SaleItem = await GetByIdAsync(id, cancellationToken);
        if (SaleItem == null)
            return false;

        if (SaleItem.DeletedAt != null)
            return false;

        try
        {
            _context.SaleItems.Remove(SaleItem);
        }
        catch
        {
            SaleItem.DeletedAt = DateTime.UtcNow;
            _context.SaleItems.Update(SaleItem);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }


    /// <summary>
    /// Retrieves the total count of SaleItems in the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>The total count of SaleItems</returns>
    public async Task<int> GetTotalSaleItemsCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        if (activeRecordsOnly)
        {
            return await _context.SaleItems
                .Where(b => b.DeletedAt == null) // Assuming there is a DeletedAt property in SaleItem
                .CountAsync(cancellationToken);
        }
        // If not filtering by active records, return the total count of SaleItems
        return await _context.SaleItems.CountAsync(cancellationToken);

    }

    /// <summary>
    /// Retrieves all active SaleItems for a specified sale
    /// </summary>
    /// <param name="saleId">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of active SaleItems for the specified sale</returns>
    public async Task<IEnumerable<SaleItem>> GetActiveItemsBySaleIdAsync(Guid saleId, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .Where(si => si.SaleId == saleId && si.DeletedAt == null) // Assuming DeletedAt indicates inactive items
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Checks if a product is already launched for the specified sale
    /// </summary>
    /// <param name="saleId">The unique identifier of the sale</param>
    /// <param name="productId">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product is already launched for the sale, false otherwise</returns>
    public async Task<bool> IsProductLaunchedForSaleAsync(Guid saleId, Guid productId, CancellationToken cancellationToken = default)
    {
        return await _context.SaleItems
            .AnyAsync(sp => sp.SaleId == saleId && sp.ProductId == productId, cancellationToken);
    }


    /// <summary>
    /// Retrieves pagination information for SaleItems
    /// </summary>
    /// <param name="pageSize">The size of each page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of SaleItems and total pages</returns>
    public async Task<(int totalSaleItems, int totalPages)> GetSaleItemsPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        // Validate pageSize
        if (pageSize <= 0)
            throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));

        // Get the total count of SaleItems
        var totalSaleItems = await GetTotalSaleItemsCountAsync(activeRecordsOnly, cancellationToken);


        // If there are no Sales, return 0 for both totalSaleItems and totalPages
        // This is important to avoid division by zero in the next step
        // and to ensure that the pagination logic works correctly
        // e.g., if totalSaleItems = 0 and pageSize = 10, totalPages = 0
        if (totalSaleItems == 0)
            return (0, 0);

        // Calculate total pages
        var totalPages = (int)Math.Ceiling((double)totalSaleItems / pageSize);

        return (totalSaleItems, totalPages);
    }
} 

