using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Repository;

public class UserRepositoryTest
{
    public DefaultContext StartDbContextInMemory()
    {
        var options = new DbContextOptionsBuilder<DefaultContext>()
            .UseInMemoryDatabase(databaseName: "BancoDeTeste")
            .Options;

        return new DefaultContext(options);
    }

    [Fact(DisplayName = "Create User")]
    [Trait("Category", "UserRepository")]
    public async Task CreateUserTest()
    {
        var context = StartDbContextInMemory();
        var userRepository = new UserRepository(context);

        var user = new Ambev.DeveloperEvaluation.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Phone = "1234567890",
            Role = UserRole.None,
            Status = UserStatus.Active,
            Email = "testuser@example.com",
            Password = "password",
        };

        var createdUser = await userRepository.CreateAsync(user);
        await context.SaveChangesAsync();
        Assert.NotNull(createdUser);

        var date = DateTime.UtcNow;

        var userFromDb = await context.Users.FirstOrDefaultAsync(u => u.Id == createdUser.Id);
        Assert.NotNull(userFromDb);

        Assert.Equal(user.Id, userFromDb.Id);
        Assert.Equal(user.Username, userFromDb.Username);   
        Assert.Equal(user.Email, userFromDb.Email);
        Assert.Equal(user.Status, userFromDb.Status);
        Assert.Equal(user.Phone, userFromDb.Phone);
        Assert.Equal(user.Role, userFromDb.Role);
        Assert.Equal(user.Password, userFromDb.Password);

        Assert.Equal(date.Date, userFromDb.CreatedAt.Date);
        Assert.NotNull(userFromDb.UpdatedAt);
        Assert.Equal(date.Date, userFromDb.UpdatedAt.Value.Date);
        
        Assert.Equal(1, context.Users.Count());
    }

    [Fact(DisplayName = "Delete User")]
    [Trait("Category", "UserRepository")]
    public async Task DeleteUserTest()
    {
        var context = StartDbContextInMemory();
        var userRepository = new UserRepository(context);

        var user = new Ambev.DeveloperEvaluation.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            Phone = "1234567890",
            Role = UserRole.None,
            Status = UserStatus.Active,
            Email = "testuser@example.com",
            Password = "password",
        };

        var createdUser = await userRepository.CreateAsync(user);
        await context.SaveChangesAsync();
        Assert.NotNull(createdUser);

        var date = DateTime.UtcNow;

        var userFromDb = await context.Users.FirstOrDefaultAsync(u => u.Id == createdUser.Id);
        Assert.NotNull(userFromDb);

        await userRepository.DeleteAsync(userFromDb.Id);
        await context.SaveChangesAsync();

        userFromDb = await context.Users.FirstOrDefaultAsync(u => u.Id == createdUser.Id);
        Assert.Null(userFromDb);

    }


}