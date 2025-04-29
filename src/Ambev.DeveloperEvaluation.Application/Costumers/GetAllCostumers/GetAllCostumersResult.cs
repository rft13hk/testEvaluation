using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;

/// <summary>
/// Represents the result of a request to retrieve a paginated list of Costumers.
/// </summary>
public class GetAllCostumersResult
{
    /// <summary>
    /// Gets or sets the list of Costumer details for the current page.
    /// </summary>
    public IEnumerable<CostumerDto> Costumers { get; set; } = new List<CostumerDto>();

    /// <summary>
    /// Gets or sets the total number of Costumers available in the system.
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
    /// This is used to shape the data returned in the GetAllCostumersResult.
    /// </summary>
    public class CostumerDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the Costumer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the Costumer.
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
        /// If not informed, assumes the universal date/time of where the system is running.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; } = null;

        /// <summary>
        /// Gets or sets the ID of the user who created this Costumer.
        /// </summary>
        public Guid UserId { get; set; }

    }
}