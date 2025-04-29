using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;

/// <summary>
/// Handler for processing GetBranchCommand requests
/// </summary>
public class GetAllBranchesHandler : IRequestHandler<GetAllBranchesCommand, GetAllBranchesResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetAllBranchesHandler
    /// </summary>
    /// <param name="BranchRepository">The Branch repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetBranchCommand</param>
    public GetAllBranchesHandler(
        IBranchRepository BranchRepository,
        IMapper mapper)
    {
        _branchRepository = BranchRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllBranchesCommand request (Get paginated list)
    /// </summary>
    /// <param name="request">The GetAllBranches command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of Branch details</returns>
    public async Task<GetAllBranchesResult> Handle(GetAllBranchesCommand request, CancellationToken cancellationToken)
    {
        var branches = await _branchRepository.GetAllAsync(request.Page, request.Size, request.Order, request.ActiveRecordsOnly, cancellationToken);
        var totalBranches = await _branchRepository.GetTotalBranchesCountAsync(request.ActiveRecordsOnly, cancellationToken);
        
        if (branches == null || !branches.Any())
        {
            return new GetAllBranchesResult
            {
                TotalItems = 0,
                TotalPages = 0,
                CurrentPage = request.Page,
                Branches = Enumerable.Empty<GetAllBranchesResult.BranchDto>()
            };
        }
        // Calculate total pages based on the total number of branches and the page size
        var totalPages = (int)Math.Ceiling((double)totalBranches / request.Size);

        var branchResults = _mapper.Map<IEnumerable<GetAllBranchesResult.BranchDto>>(branches);

        return new GetAllBranchesResult
        {
            TotalItems = totalBranches,
            TotalPages = totalPages,
            CurrentPage = request.Page,
            Branches = branchResults
        };
    }

}
