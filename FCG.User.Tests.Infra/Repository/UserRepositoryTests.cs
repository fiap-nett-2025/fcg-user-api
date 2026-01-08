using Microsoft.EntityFrameworkCore;
using FCG.User.Domain.ValueObjects;
using FCG.User.Domain.Entities;
using FCG.User.Infra.Data.Repository;
using FCG.User.Infra.Data.Context;

namespace FCG.User.Tests.Infra.Repository;

public class UserRepositoryTests
{
    private readonly IDbContextFactory<FCGDbContext> _contextFactory;
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<FCGDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
            .Options;

        // mock/fake do IDbContextFactory
        _contextFactory = new TestDbContextFactory(options);
        _repository = new UserRepository(_contextFactory);
    }

    public class TestDbContextFactory : IDbContextFactory<FCGDbContext>
    {
        private readonly DbContextOptions<FCGDbContext> _options;

        public TestDbContextFactory(DbContextOptions<FCGDbContext> options)
        {
            _options = options;
        }

        public FCGDbContext CreateDbContext()
        {
            return new FCGDbContext(_options);
        }
    }

    [Fact]
    public async Task AddAsync_ValidUser_ShouldAddUserToDatabase()
    {
        using var dbContext = _contextFactory.CreateDbContext();

        //  Arrange
        var user = new User.Domain.Entities.User("José Silva", "rm000000@fiap.com.br");

        // Act
        await _repository.AddAsync(user);

        // Assert
        var saveUser = await dbContext.Users.FirstOrDefaultAsync();
        Assert.NotNull(saveUser);
        Assert.Equal("rm000000@fiap.com.br", saveUser.Email!);
    }

    [Fact]
    public async Task DeleteAsync_ExistUser_ShouldDeleteUserFromDatabase()
    {
        using var dbContext = _contextFactory.CreateDbContext();

        // Arrange
        var user = new User.Domain.Entities.User("José Silva", "rm000000@fiap.com.br");

        // Act
        await _repository.AddAsync(user);
        await _repository.DeleteAsync(user.Id!);

        // Assert
        Assert.Null(await dbContext.Users.FindAsync(user.Id));
    }

    [Fact]
    public async Task UpdateAsync_ExistUser_ShouldUpdateUser()
    {
        // Arrange
        var user = new User.Domain.Entities.User("José Silva", "rm000000@fiap.com.br");

        // Act
        await _repository.AddAsync(user);
        user.Email = new Email("professor@pm3.com.br").ToString();
        await _repository.UpdateAsync(user);

        // Assert
        Assert.True(await _repository.ExistsByEmailAsync("professor@pm3.com.br"));
    }
}
