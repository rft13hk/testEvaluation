using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.GetAllSaleItems;
using Ambev.DeveloperEvaluation.WebApi.Features.SaleItems.DeleteSaleItem;
using Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetSaleItem;
using Ambev.DeveloperEvaluation.Application.SaleItems.GetAllSaleItems;
using Ambev.DeveloperEvaluation.Application.SaleItems.DeleteSaleItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.SaleItems;

/// <summary>
/// Controller for managing SaleItem operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SaleItemsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SaleItemsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SaleItemsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new SaleItem
    /// </summary>
    /// <param name="request">The SaleItem creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created SaleItem details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleItemResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSaleItem([FromBody] CreateSaleItemRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleItemCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateSaleItemResponse>
        {
            Success = true,
            Message = "SaleItem created successfully",
            Data = _mapper.Map<CreateSaleItemResponse>(response)
        });
    }


    /// <summary>
    /// Retrieves a SaleItem by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the SaleItem</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The SaleItem details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleItem([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleItemRequest { Id = id };
        var validator = new GetSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleItemCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetSaleItemResponse>
        {
            Success = true,
            Message = "SaleItem retrieved successfully",
            Data = _mapper.Map<GetSaleItemResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a paginated list of all SaleItems
    /// </summary>
    /// <param name="_page">Page number for pagination (default: 1)</param>
    /// <param name="_size">Number of items per page (default: 10)</param>
    /// <param name="_order">Ordering of results (e.g., "SaleItemName desc, CreateAt asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of SaleItem details</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<GetAllSaleItemsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSaleItems([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string? _order = null, [FromQuery] bool _activeRecordsOnly = true, CancellationToken cancellationToken = default)
    {
        var command = new GetAllSaleItemsCommand
        {
            Page = _page,
            Size = _size,
            Order = _order,
            ActiveRecordsOnly = _activeRecordsOnly
        };

        var result = await _mediator.Send(command, cancellationToken);

        var responseData = new GetAllSaleItemsResponse()
        {
            TotalItems = result.TotalItems,
            TotalPages = result.TotalPages,
            CurrentPage = result.CurrentPage,
            SaleItems = _mapper.Map<IEnumerable<GetAllSaleItemsResponse.SaleItemDto>>(result.SaleItems)
        };

        return Ok(responseData);
    }

    /// <summary>
    /// Deletes a SaleItem by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the SaleItem to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the SaleItem was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSaleItem([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleItemRequest { Id = id };
        var validator = new DeleteSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleItemCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "SaleItem deleted successfully"
        });
    }



}
