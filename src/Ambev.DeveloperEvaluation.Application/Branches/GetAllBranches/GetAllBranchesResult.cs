using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;

/// <summary>
/// Represents the result of a request to retrieve a paginated list of branches.
/// </summary>
public class GetAllBranchesResult
{
    /// <summary>
    /// Gets or sets the list of branch details for the current page.
    /// </summary>
    public IEnumerable<BranchDto> Branches { get; set; } = new List<BranchDto>();

    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a Branch.
    /// This is used to shape the data returned in the GetAllBranchesResult.
    /// </summary>
    public class BranchDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the branch.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch.
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the universal Date/Time of when the Branch was created.
        /// </summary>
        public DateTimeOffset CreateAt { get; set; }

        /// <summary>
        /// Gets or sets the universal Date/Time of when the Branch was last updated.
        /// </summary>
        public DateTimeOffset UpdateAt { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created this branch.
        /// </summary>
        public Guid UserId { get; set; }
    }
}