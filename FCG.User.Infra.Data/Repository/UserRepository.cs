using FCG.User.Domain.Entities;
using FCG.User.Domain.Interfaces;
using FCG.User.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.User.Infra.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly FCGDbContext _context;

    public UserRepository(FCGDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Domain.Entities.User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var user = await GetByIdAsync(id);
        if (user is not null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email);
    }

    public async Task<Domain.Entities.User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Domain.Entities.User?> GetByIdAsync(string id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateAsync(Domain.Entities.User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
