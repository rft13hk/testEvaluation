using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Costumers.GetAllCostumers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Unit.Application.CostumerTest.GetAllCostumers
{
    public class GetAllCostumersHandlerTests
    {
        private readonly ICostumerRepository _mockCostumerRepository;
        private readonly IMapper _mockMapper;
        private readonly GetAllCostumersHandler _handler;

        public GetAllCostumersHandlerTests()
        {
            _mockCostumerRepository = Substitute.For<ICostumerRepository>();
            _mockMapper = Substitute.For<IMapper>();
            _handler = new GetAllCostumersHandler(_mockCostumerRepository, _mockMapper);
        }

        [Trait("Category", "ApplicationCostumer")]
        [Fact(DisplayName = "GetAllCostumers - Handle - Should return paginated result when branches exist")]
        public async Task Handle_Should_ReturnPaginatedResult_When_CostumersExist()
        {
            // Arrange
            var branches = new List<Ambev.DeveloperEvaluation.Domain.Entities.Costumer>
            {
                new Ambev.DeveloperEvaluation.Domain.Entities.Costumer 
                { 
                    Id = Guid.NewGuid(), 
                    CostumerName = "Costumer 1", 
                    CreateAt = DateTimeOffset.UtcNow, 
                    UpdateAt = DateTimeOffset.UtcNow,
                    UserId = Guid.NewGuid(),
                    Users = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "User 1",
                        Email = "user1@example.com"
                    }
                },
                new Ambev.DeveloperEvaluation.Domain.Entities.Costumer 
                { 
                    Id = Guid.NewGuid(), 
                    CostumerName = "Costumer 2", 
                    CreateAt = DateTimeOffset.UtcNow, 
                    UpdateAt = DateTimeOffset.UtcNow,
                    UserId = Guid.NewGuid(),
                    Users = new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "User 1",
                        Email = "user1@example.com"
                    }

                }
            };

            var branchDtos = branches.Select(b => new GetAllCostumersResult.CostumerDto
            {
                Id = b.Id,
                CostumerName = b.CostumerName,
                CreateAt = b.CreateAt,
                UpdateAt = b.UpdateAt
            });

            _mockCostumerRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.Costumer>)branches));

            _mockCostumerRepository.GetTotalCostumersCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(2));

            _mockMapper.Map<IEnumerable<GetAllCostumersResult.CostumerDto>>(branches)
                .Returns(branchDtos);

            var command = new GetAllCostumersCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalItems);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(branchDtos, result.Costumers);
        }

        [Trait("Category", "ApplicationCostumer")]
        [Fact(DisplayName = "GetAllCostumers - Handle - Should return empty result when no branches exist")]
        public async Task Handle_Should_ReturnEmptyResult_When_NoCostumersExist()
        {

            var branchDtos = Enumerable.Empty<DeveloperEvaluation.Domain.Entities.Costumer>();

            // Arrange
            _mockCostumerRepository.GetAllAsync(1, 10, null, true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult((IEnumerable<DeveloperEvaluation.Domain.Entities.Costumer>)branchDtos));

            _mockCostumerRepository.GetTotalCostumersCountAsync(true, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(0));

            var command = new GetAllCostumersCommand { Page = 1, Size = 10, ActiveRecordsOnly = true };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Costumers);
            Assert.Equal(0, result.TotalItems);
            Assert.Equal(0, result.TotalPages);
            Assert.Equal(1, result.CurrentPage);
        }

        [Trait("Category", "ApplicationCostumer")]
        [Fact(DisplayName = "GetAllCostumers - Handle - Should call repository with correct parameters")]
        public async Task Handle_Should_CallRepository_WithCorrectParameters()
        {
            // Arrange
            var command = new GetAllCostumersCommand { Page = 2, Size = 5, Order = "Name asc", ActiveRecordsOnly = false };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _mockCostumerRepository.Received(1).GetAllAsync(2, 5, "Name asc", false, Arg.Any<CancellationToken>());
            await _mockCostumerRepository.Received(1).GetTotalCostumersCountAsync(false, Arg.Any<CancellationToken>());
        }
    }
}