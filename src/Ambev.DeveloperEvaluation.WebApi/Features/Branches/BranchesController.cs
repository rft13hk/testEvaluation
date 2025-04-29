using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetAllBranches;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;
using Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches;

/// <summary>
/// Controller for managing Branch operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BranchesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of BranchesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public BranchesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new Branch
    /// </summary>
    /// <param name="request">The Branch creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Branch details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBranch([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateBranchRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateBranchCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateBranchResponse>
        {
            Success = true,
            Message = "Branch created successfully",
            Data = _mapper.Map<CreateBranchResponse>(response)
        });
    }


    /// <summary>
    /// Retrieves a Branch by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Branch details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetBranchResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetBranchRequest { Id = id };
        var validator = new GetBranchRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetBranchCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetBranchResponse>
        {
            Success = true,
            Message = "Branch retrieved successfully",
            Data = _mapper.Map<GetBranchResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a paginated list of all Branches
    /// </summary>
    /// <param name="_page">Page number for pagination (default: 1)</param>
    /// <param name="_size">Number of items per page (default: 10)</param>
    /// <param name="_order">Ordering of results (e.g., "BranchName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of Branch details</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetAllBranchesResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBranches([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string? _order = null, [FromQuery] bool _activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        var command = new GetAllBranchesCommand
        {
            Page = _page,
            Size = _size,
            Order = _order,
            ActiveRecordsOnly = _activeRecordsOnly
        };

        var result = await _mediator.Send(command, cancellationToken);

        var responseData = new GetAllBranchesResponse()
        {
            TotalItems = result.TotalItems,
            TotalPages = result.TotalPages,
            CurrentPage = result.CurrentPage,
            Branches = _mapper.Map<IEnumerable<GetAllBranchesResponse.BranchDto>>(result.Branches)
        };

        return Ok(responseData);
    }

    /// <summary>
    /// Deletes a Branch by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the Branch was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBranch([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteBranchRequest { Id = id };
        var validator = new DeleteBranchRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteBranchCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Branch deleted successfully"
        });
    }



}
