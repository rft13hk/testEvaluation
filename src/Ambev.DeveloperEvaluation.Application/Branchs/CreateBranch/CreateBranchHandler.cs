using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Handler for processing CreateBranchCommand requests.
/// </summary>
public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, CreateBranchResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUserRepository _userRepository; // Assuming you need to validate the user
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateBranchHandler.
    /// </summary>
    /// <param name="branchRepository">The branch repository.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateBranchHandler(IBranchRepository branchRepository, IUserRepository userRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateBranchCommand request.
    /// </summary>
    /// <param name="command">The CreateBranch command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created branch details.</returns>
    public async Task<CreateBranchResult> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateBranchValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Validate if the user creating the branch exists
        var creatingUser = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (creatingUser == null)
            throw new InvalidOperationException($"User with ID {command.UserId} not found.");

        var branch = _mapper.Map<Branch>(command);
        branch.UserId = command.UserId; // Ensure UserId is set
        branch.CreateAt = DateTimeOffset.UtcNow;
        branch.UpdateAt = DateTimeOffset.UtcNow;

        var createdBranch = await _branchRepository.CreateAsync(branch, cancellationToken);
        var result = _mapper.Map<CreateBranchResult>(createdBranch);
        return result;
    }
}