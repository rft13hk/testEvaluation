using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Branches.GetAllBranches;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.GetAllBranches
{
    public class GetAllBranchesHandlerTests
    {
        private readonly IBranchRepository _mockBranchRepository;
        private readonly IMapper _mockMapper;
        private readonly GetAllBranchesHandler _handler;

        public GetAllBranchesHandlerTests()
        {
            _mockBranchRepository = Substitute.For<IBranchRepository>();
            _mockMapper = Substitute.For<IMapper>();
            _handler = new GetAllBranchesHandler(_mockBranchRepository, _mockMapper);
        }

        [Trait("Category", "ApplicationBranch")]
        [Fact(DisplayName = "GetAllBranches - Handle - Should return paginated result when branches exist")]
        public async Task Handle_Should_ReturnPaginatedResult_When_BranchesExist()
        {
            // Arrange
            var branches = new List<Ambev.DeveloperEvaluation.Domain.Entities.Branch>
            {
                new Ambev.DeveloperEvaluation.Domain.Entities.Branch 
                { 
                    Id = Guid.NewGuid(), 
                    BranchName = "Branch 1", 
                    CreateAt = DateTimeOffset.UtcNow, 
                    UpdateAt = DateTimeOffset.UtcNow,
                    UserId = Guid.NewGuid(),
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "User 1",
                        Email = "user1@example.com"
                    }
                },
                new Ambev.DeveloperEvaluation.Domain.Entities.Branch 
                { 
                    Id = Guid.NewGuid(), 
                    BranchName = "Branch 2", 
                    CreateAt = DateTimeOffset.UtcNow, 
                    UpdateAt = DateTimeOffset.UtcNow,
                    UserId = Guid.NewGuid(),
                    User = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "User 1",
                        Email = "user1@example.com"
                    }

                }
            };

            var branchDtos = branches.Select(b => new GetAllBranchesResult.BranchDto
            {
                Id = b.Id,
                BranchName = b.BranchName,
                CreateAt = b.CreateAt,
                UpdateAt = b.UpdateAt
            });

            _mockBranchRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.Branch>)branches));

            _mockBranchRepository.GetTotalBranchesCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(2));

            _mockMapper.Map<IEnumerable<GetAllBranchesResult.BranchDto>>(branches)
                .Returns(branchDtos);

            var command = new GetAllBranchesCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalItems);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(branchDtos, result.Branches);
        }

        [Trait("Category", "ApplicationBranch")]
        [Fact(DisplayName = "GetAllBranches - Handle - Should return empty result when no branches exist")]
        public async Task Handle_Should_ReturnEmptyResult_When_NoBranchesExist()
        {

            var branchDtos = Enumerable.Empty<DeveloperEvaluation.Domain.Entities.Branch>();

            // Arrange
            _mockBranchRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.Branch>)branchDtos));

            _mockBranchRepository.GetTotalBranchesCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(0));

            var command = new GetAllBranchesCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Branches);
            Assert.Equal(0, result.TotalItems);
            Assert.Equal(0, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
        }

        [Trait("Category", "ApplicationBranch")]
        [Fact(DisplayName = "GetAllBranches - Handle - Should call repository with correct parameters")]
        public async Task Handle_Should_CallRepository_WithCorrectParameters()
        {
            // Arrange
            var command = new GetAllBranchesCommand { Page = 2, Size = 5, Order = "Name asc", ActiveRecordsOnly = false };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _mockBranchRepository.Received(1).GetAllAsync(2, 5, "Name asc", false, Arg.Any<CancellationToken>());
            await _mockBranchRepository.Received(1).GetTotalBranchesCountAsync(false, Arg.Any<CancellationToken>());
        }
    }
}