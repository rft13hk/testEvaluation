using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

/// <summary>
/// Represents the result of a request to retrieve a paginated list of sales.
/// </summary>
public class GetAllSalesResponse
{
    /// <summary>
    /// Gets or sets the list of sale details for the current page.
    /// </summary>
    public IEnumerable<SaleDto> Sales { get; set; } = new List<SaleDto>();

    /// <summary>
    /// Gets or sets the total number of sales available in the system.
    /// This is used for pagination purposes.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages available based on the page size.
    /// This is used for pagination purposes.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the current page number.
    /// </summary>
    public int CurrentPage { get; set; } = 1;


    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a Sale.
    /// This is used to shape the data returned in the GetAllSalesResponse.
    /// </summary>
    public class SaleDto
    {
        /// <summary>
        /// Represents the unique identifier of the Sale.
        /// Must be valid and not null or empty.
        /// </summary>
        public Guid Id { get; set; } = Guid.Empty;

        /// <summary>
        /// Represents the Sale number (SKU).
        /// Must be valid and not null or empty.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets the Sale Date
        /// Must not be null or empty.
        /// </summary>
        public DateTimeOffset SaleDate { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Represents the Product code (SKU).
        /// Must be valid and not null or empty.
        /// </summary>
        public Guid CostumerId { get; set; } = Guid.Empty;

        /// <summary>
        /// Represents the Product code (SKU).
        /// Must be valid and not null or empty.
        /// </summary>
        public Guid BranchId { get; set; } = Guid.Empty;

        /// <summary>
        /// Represents the universal Date/Time of when the Sale was created;
        /// If not informed, assumes the universal date/time of where the system is running;
        /// </summary>
        public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Represents the universal Date/Time of when the Sale was updated;
        /// If not informed, assumes the universal date/time of where the system is running;
        /// </summary>
        public DateTimeOffset UpdateAt { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Represents the universal Date/Time of when the Sale was deleted;
        /// If not informed, assumes the universal date/time of where the system is running;
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; } = null;

        /// <summary>
        /// Indicates which user created this Sale.
        /// </summary>
        public Guid UserId { get; set; } = Guid.Empty;

        /// <summary>
        /// Indicates the status of the Sale.
        /// </summary>
        public int TotalItems { get; set; } = 0;

        /// <summary>
        /// Represents the total value of the Sale.
        /// </summary>
        public Decimal TotalValue { get; set; } = 0.0M;

        /// <summary>
        /// Represents the total discount applied to the Sale.
        /// </summary>
        public Decimal TotalDiscount { get; set; } = 0.0M;

        /// <summary>
        /// Represents the total discount applied to the Sale.
        /// </summary>
        public Decimal TotalWithDiscount { get; set; } = 0.0M;



    }
}