using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.GetAllSaleItems;

/// <summary>
/// Represents the result of a request to retrieve a paginated list of SaleItems.
/// </summary>
public class GetAllSaleItemsResult
{
    /// <summary>
    /// Gets or sets the list of SaleItem details for the current page.
    /// </summary>
    public IEnumerable<SaleItemDto> SaleItems { get; set; } = new List<SaleItemDto>();

    /// <summary>
    /// Gets or sets the total number of SaleItems available in the system.
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
    /// Represents a Data Transfer Object (DTO) for a SaleItem.
    /// This is used to shape the data returned in the GetAllSaleItemsResult.
    /// </summary>
    public class SaleItemDto
    {
        /// <summary>
        /// Represents the unique identifier of the Sale.
        /// Must be valid and not null or empty.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets the Sale Date
        /// Must not be null or empty.
        /// </summary>
        public DateTimeOffset SaleDate { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Represents the unique identifier of the SaleItem.
        /// Must be valid and not null or empty.
        /// </summary>
        public Guid SaleItemId { get; set; }

        /// <summary>
        /// represents the unit price of the saleitem.
        /// Must be valid and not null or empty.
        /// </summary>
        public decimal Price { get; set; } = 0.0m;

        /// <summary>
        /// Represents the quantity of the saleitem.
        /// Must be valid and not null or empty.
        /// </summary>
        public int Quantity { get; set; } = 0;

        /// <summary>
        /// Represents the discount applied to the saleitem.
        /// Must be valid and not null or empty.
        /// </summary>
        public decimal Discount { get; set; } = 0.0m;

        /// <summary>
        /// Represents the total price of the saleitem.
        /// Must be valid and not null or empty.
        /// </summary>
        public decimal TotalPrice { get; set; } = 0.0m;


        public SaleItemStatus StatusItem { get; set; }

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
    }
}