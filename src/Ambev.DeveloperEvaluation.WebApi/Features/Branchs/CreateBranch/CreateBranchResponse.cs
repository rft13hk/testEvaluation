using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch;

/// <summary>
/// API response model for CreateBranch operation
/// </summary>
public class CreateBranchResponse
{
        /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }
}
