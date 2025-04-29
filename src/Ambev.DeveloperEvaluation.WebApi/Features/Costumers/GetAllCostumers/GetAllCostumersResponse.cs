using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetAllCostumers;

/// <summary>
/// Represents the result of a request to retrieve a paginated list of costumers.
/// </summary>
public class GetAllCostumersResponse
{
    /// <summary>
    /// Gets or sets the list of costumer details for the current page.
    /// </summary>
    public IEnumerable<CostumerDto> Costumers { get; set; } = new List<CostumerDto>();

        /// <summary>
    /// Gets or sets the total number of costumers available in the system.
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
    /// Represents a Data Transfer Object (DTO) for a Costumer.
    /// This is used to shape the data returned in the GetAllCostumersResponse.
    /// </summary>
    public class CostumerDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the costumer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the costumer.
        /// </summary>
        public string CostumerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the universal Date/Time of when the Costumer was created.
        /// </summary>
        public DateTimeOffset CreateAt { get; set; }

        /// <summary>
        /// Gets or sets the universal Date/Time of when the Costumer was last updated.
        /// </summary>
        public DateTimeOffset UpdateAt { get; set; }

        /// <summary>
        /// Gets or sets the universal Date/Time of when the Costumer was deleted.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; } = null;

        /// <summary>
        /// Gets or sets the ID of the user who created this costumer.
        /// </summary>
        public Guid UserId { get; set; }
    }
}