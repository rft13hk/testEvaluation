using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Branch;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateBranchHandlerTestData
{

    /// <summary>
    /// Generates a valid CreateBranchCommand object using the Bogus library.
    /// The generated command will have a random branch name.
    /// </summary>
    private static readonly Faker<CreateBranchCommand> createBranchHandlerFaker = new Faker<CreateBranchCommand>()
        .RuleFor(u => u.BranchName, f => f.Company.CompanyName());

    /// <summary>
    /// Generates a valid CreateBranchCommand object with randomized data.
    /// The generated command will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateBranchCommand object with randomly generated data.</returns>
    public static CreateBranchCommand GenerateValidCommand()
    {
        // Generate a valid command with a random branch name
        var command = createBranchHandlerFaker.Generate();
        command.UserId = Guid.NewGuid(); // Assign a random user ID
        return command;
    }
}
