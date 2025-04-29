using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Costumers.CreateCostumer;
using Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetCostumer;
using Ambev.DeveloperEvaluation.WebApi.Features.Costumers.GetAllCostumers;
using Ambev.DeveloperEvaluation.WebApi.Features.Costumers.DeleteCostumer;
using Ambev.DeveloperEvaluation.Application.Costumers.CreateCostumer;
using Ambev.DeveloperEvaluation.Application.Costumers.GetCostumer;
using Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;
using Ambev.DeveloperEvaluation.Application.Costumers.DeleteCostumer;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Costumers;

/// <summary>
/// Controller for managing Costumer operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CostumersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CostumersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CostumersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new Costumer
    /// </summary>
    /// <param name="request">The Costumer creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Costumer details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCostumerResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCostumer([FromBody] CreateCostumerRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCostumerRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCostumerCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCostumerResponse>
        {
            Success = true,
            Message = "Costumer created successfully",
            Data = _mapper.Map<CreateCostumerResponse>(response)
        });
    }


    /// <summary>
    /// Retrieves a Costumer by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Costumer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Costumer details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCostumerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCostumer([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetCostumerRequest { Id = id };
        var validator = new GetCostumerRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCostumerCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetCostumerResponse>
        {
            Success = true,
            Message = "Costumer retrieved successfully",
            Data = _mapper.Map<GetCostumerResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a paginated list of all Costumers
    /// </summary>
    /// <param name="_page">Page number for pagination (default: 1)</param>
    /// <param name="_size">Number of items per page (default: 10)</param>
    /// <param name="_order">Ordering of results (e.g., "CostumerName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of Costumer details</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetAllCostumersResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCostumers([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string? _order = null, [FromQuery] bool _activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        var command = new GetAllCostumersCommand
        {
            Page = _page,
            Size = _size,
            Order = _order,
            ActiveRecordsOnly = _activeRecordsOnly
        };

        var result = await _mediator.Send(command, cancellationToken);

        var responseData = new GetAllCostumersResponse()
        {
            TotalItems = result.TotalItems,
            TotalPages = result.TotalPages,
            CurrentPage = result.CurrentPage,
            Costumers = _mapper.Map<IEnumerable<GetAllCostumersResponse.CostumerDto>>(result.Costumers)
        };

        return Ok(responseData);
    }

    /// <summary>
    /// Deletes a Costumer by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the Costumer to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the Costumer was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCostumer([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCostumerRequest { Id = id };
        var validator = new DeleteCostumerRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCostumerCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Costumer deleted successfully"
        });
    }



}
