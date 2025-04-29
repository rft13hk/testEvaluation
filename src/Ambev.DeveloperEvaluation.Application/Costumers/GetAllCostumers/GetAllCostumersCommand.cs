using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;

/// <summary>
/// Command to retrieve a paginated list of Costumers.
/// </summary>
public class GetAllCostumersCommand : IRequest<GetAllCostumersResult>
{
    

    /// <summary>
    /// Gets or sets the page number for pagination (default: 1).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of items per page (default: 10).
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the ordering of results (e.g., "CostumerName desc, CreateAt asc").
    /// Default is ascending if not specified.
    /// </summary>
    public string? Order { get; set; }


    /// <summary>
    /// Gets or sets a value indicating whether to return only active records.
    /// If true, only active records will be returned (default: true).
    /// </summary>
    public bool ActiveRecordsOnly  { get; set; } = true;
    
}