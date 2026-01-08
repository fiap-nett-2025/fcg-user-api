using FCG.User.Domain.Entities;
using FCG.User.Domain.Interfaces;
using FCG.User.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.User.Infra.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<FCGDbContext> _contextFactory;

    public UserRepository(IDbContextFactory<FCGDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task AddAsync(Domain.Entities.User user)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        var user = await GetByIdAsync(id);
        if (user is not null)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        return await dbContext.Users
            .AnyAsync(u => u.Email == email);
    }

    public async Task<Domain.Entities.User?> GetByEmailAsync(string email)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Domain.Entities.User?> GetByIdAsync(string id)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        return await dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateAsync(Domain.Entities.User user)
    {
        using var dbContext = _contextFactory.CreateDbContext();

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }
}
