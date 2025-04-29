using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// Represents the result of a request to retrieve a paginated list of products.
/// </summary>
public class GetAllProductsResponse
{
    /// <summary>
    /// Gets or sets the list of product details for the current page.
    /// </summary>
    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();

        /// <summary>
    /// Gets or sets the total number of products available in the system.
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
    /// Represents a Data Transfer Object (DTO) for a Product.
    /// This is used to shape the data returned in the GetAllProductsResponse.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the universal Date/Time of when the Product was created.
        /// </summary>
        public DateTimeOffset CreateAt { get; set; }

        /// <summary>
        /// Gets or sets the universal Date/Time of when the Product was last updated.
        /// </summary>
        public DateTimeOffset UpdateAt { get; set; }

        /// <summary>
        /// Gets or sets the universal Date/Time of when the Product was deleted.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; } = null;

        /// <summary>
        /// Gets or sets the ID of the user who created this product.
        /// </summary>
        public Guid UserId { get; set; }
    }
}