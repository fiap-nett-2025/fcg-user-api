using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Infra.Data.Repository
{
    public class UserGameLibraryRepository : IUserGameLibraryRepository
    {
        private readonly FCGDbContext _context;

        public UserGameLibraryRepository(FCGDbContext context)
        {
            _context = context;
        }

        public async Task AddGameToUserLibraryAsync(UserGameLibrary userGameLibrary)
        {
            await _context.UserGameLibraries.AddAsync(userGameLibrary);
            await _context.SaveChangesAsync();
        }

        public async Task<UserGameLibrary?> GetOneGameFromUserLibraryAsync(string userId, int gameId)
        {
            var entity = await _context.UserGameLibraries.FindAsync([userId, gameId]);
            return entity;
        }

        //ver se mantem esse update
        public Task UpdateGameInUserLibraryAsync(UserGameLibrary userGameLibrary)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<UserGameLibrary>> GetGamesLibraryByUserIdAsync(string userId)
        {
            return await _context.UserGameLibraries
                   .AsNoTracking()
                   .Where(x => x.UserId == userId)
                   .OrderByDescending(x => x.AddedAt)
                   .ToListAsync();
        }

        public async Task RemoveGameFromUserLibraryAsync(string userId, int gameId)
        {
            var entity = await _context.UserGameLibraries.FindAsync(userId, gameId);
            if (entity is null) return;

            _context.UserGameLibraries.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAllRecordsAsync(string userId)
        {
            await _context.UserGameLibraries.
                Where(x => x.UserId == userId)
                .ExecuteDeleteAsync();
        }
    }
}
