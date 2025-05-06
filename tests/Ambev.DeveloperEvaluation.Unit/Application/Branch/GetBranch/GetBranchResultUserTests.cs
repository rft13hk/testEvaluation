using Ambev.DeveloperEvaluation.Application.Branches.GetBranch;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.BranchTest.GetBranch;

/// <summary>
/// Unit tests for the <see cref="GetBranchResultUser"/> class.
/// </summary>
public class GetBranchResultUserTests
{
    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Get Branch - Given a new instance When initialized Then properties have default values")]
    public void NewInstance_ShouldHaveDefaultValues()
    {
        // When
        var user = new GetBranchResultUser();

        // Then
        user.Id.Should().Be(Guid.Empty);
        user.Username.Should().Be(string.Empty);
        user.Email.Should().Be(string.Empty);
        user.Phone.Should().Be(string.Empty);
        user.Password.Should().Be(string.Empty);
        user.Role.Should().Be(UserRole.None); // Assuming `UserRole.None` is the default
        user.Status.Should().Be(UserStatus.Unknown); // Assuming `UserStatus.Unknown` is the default
        user.CreatedAt.Date.Should().Be(DateTime.UtcNow.Date);
        user.UpdatedAt!.Value.Date.Should().Be(DateTime.UtcNow.Date);
    }

    [Trait("Category", "ApplicationBranch")]
    [Fact(DisplayName = "Get Branch - Given a new instance When properties are set Then values are updated correctly")]
    public void SetProperties_ShouldUpdateValues()
    {
        // Given
        var userId = Guid.NewGuid();
        var username = "TestUser";
        var email = "testuser@example.com";
        var phone = "(11) 98765-4321";
        var password = "SecurePassword123!";
        var role = UserRole.Admin;
        var status = UserStatus.Active;
        var createdAt = new DateTime(2023, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var updatedAt = new DateTime(2023, 1, 2, 12, 0, 0, DateTimeKind.Utc);

        // When
        var user = new GetBranchResultUser
        {
            Id = userId,
            Username = username,
            Email = email,
            Phone = phone,
            Password = password,
            Role = role,
            Status = status,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };

        // Then
        user.Id.Should().Be(userId);
        user.Username.Should().Be(username);
        user.Email.Should().Be(email);
        user.Phone.Should().Be(phone);
        user.Password.Should().Be(password);
        user.Role.Should().Be(role);
        user.Status.Should().Be(status);
        user.CreatedAt.Should().Be(createdAt);
        user.UpdatedAt.Should().Be(updatedAt);
    }
}