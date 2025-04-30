using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IBranchRepository using Entity Framework Core
/// </summary>
public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of BranchRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new branch in the database
    /// </summary>
    /// <param name="branch">The branch to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created branch</returns>
    public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    /// <summary>
    /// Retrieves a branch by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The branch if found, null otherwise</returns>
    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .Include(b => b.Users) // Assuming there is a navigation property for Users in Branch
            .AsNoTracking() // Use AsNoTracking for read-only queries to improve performance
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken); // Changed to FirstOrDefaultAsync for better null handling
    }

    /// <summary>
    /// Retrieves a paginated list of branches based on the provided parameters
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="size">Number of items per page (default: 10)</param>
    /// <param name="order">Ordering of results (e.g., "BranchName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="activeRecordsOnly">If true, only active records will be returned</param>
    /// <returns>A list of branches for the requested page</returns>
    public async Task<IEnumerable<Branch>> GetAllAsync(int page = 1, int size = 10, string? order = null, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Branch> query = _context.Branches
            .Include(b => b.Users) // Assuming there is a navigation property for Users in Branch
            .AsQueryable();

        if (activeRecordsOnly)
        {
            query = query.Where(b => b.DeletedAt == null); // Assuming there is a DeletedAt property in Branch
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
            query = query.OrderBy(b => b.BranchName); // Default ordering by BranchName ascending
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
    private IQueryable<Branch> ApplyOrdering(IQueryable<Branch> query, string propertyName, string direction)
    {
        switch (propertyName.ToLower())
        {
            case "branchname":
                query = direction == "descending" ? query.OrderByDescending(b => b.BranchName) : query.OrderBy(b => b.BranchName);
                break;
            case "createat":
                query = direction == "descending" ? query.OrderByDescending(b => b.CreateAt) : query.OrderBy(b => b.CreateAt);
                break;
            case "updateat":
                query = direction == "descending" ? query.OrderByDescending(b => b.UpdateAt) : query.OrderBy(b => b.UpdateAt);
                break;
            // Add more cases for other properties you might want to order by
            default:
                query = query.OrderBy(b => b.BranchName); // Default fallback
                break;
        }
        return query;
    }

    /// <summary>
    /// Deletes a branch from the database
    /// </summary>
    /// <param name="id">The unique identifier of the branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the branch was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(id, cancellationToken);
        if (branch == null)
            return false;

        if (branch.DeletedAt != null)
            return false;

        try
        {
            _context.Branches.Remove(branch);
        }
        catch
        {
            branch.DeletedAt = DateTime.UtcNow;
            _context.Branches.Update(branch);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }


    /// <summary>
    /// Retrieves the total count of branches in the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>The total count of branches</returns>
    public async Task<int> GetTotalBranchesCountAsync(bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        if (activeRecordsOnly)
        {
            return await _context.Branches
                .Where(b => b.DeletedAt == null) // Assuming there is a DeletedAt property in Branch
                .CountAsync(cancellationToken);
        }
        // If not filtering by active records, return the total count of branches
        return await _context.Branches.CountAsync(cancellationToken);

    }

    /// <summary>
    /// Retrieves pagination information for branches
    /// </summary>
    /// <param name="pageSize">The size of each page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing the total number of branches and total pages</returns>
    public async Task<(int totalBranches, int totalPages)> GetBranchesPaginationInfoAsync(int pageSize, bool activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        // Validate pageSize
        if (pageSize <= 0)
            throw new ArgumentException("Page size must be greater than zero.", nameof(pageSize));

        // Get the total count of branches
        var totalBranches = await GetTotalBranchesCountAsync(activeRecordsOnly, cancellationToken);
        
        // If there are no branches, return 0 for both totalBranches and totalPages
        // This is important to avoid division by zero in the next step
        // and to ensure that the pagination logic works correctly
        // e.g., if totalBranches = 0 and pageSize = 10, totalPages = 0
        if (totalBranches == 0)
            return (0, 0);

        // Calculate the total number of pages
        var totalPages = (int)Math.Ceiling((double)totalBranches / pageSize);

        return (totalBranches, totalPages);
    }
} 

