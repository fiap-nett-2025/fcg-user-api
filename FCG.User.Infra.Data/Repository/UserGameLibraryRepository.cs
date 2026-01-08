using FCG.User.Domain.Entities;
using FCG.User.Domain.Interfaces;
using FCG.User.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.User.Infra.Data.Repository
{
    public class UserGameLibraryRepository : IUserGameLibraryRepository
    {
        private readonly IDbContextFactory<FCGDbContext> _contextFactory;

        public UserGameLibraryRepository(IDbContextFactory<FCGDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddGameToUserLibraryAsync(UserGameLibrary userGameLibrary)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            await dbContext.UserGameLibraries.AddAsync(userGameLibrary);
            await dbContext.SaveChangesAsync();
        }

        public async Task<UserGameLibrary?> GetOneGameFromUserLibraryAsync(string userId, string gameId)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            var entity = await dbContext.UserGameLibraries.FindAsync([userId, gameId]);
            return entity;
        }

        //ver se mantem esse update
        public Task UpdateGameInUserLibraryAsync(UserGameLibrary userGameLibrary)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<UserGameLibrary>> GetGamesLibraryByUserIdAsync(string userId)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            return await dbContext.UserGameLibraries
                   .AsNoTracking()
                   .Where(x => x.UserId == userId)
                   .OrderByDescending(x => x.AddedAt)
                   .ToListAsync();
        }

        public async Task RemoveGameFromUserLibraryAsync(string userId, string gameId)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            var entity = await dbContext.UserGameLibraries.FindAsync(userId, gameId);
            if (entity is null) return;

            dbContext.UserGameLibraries.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveAllRecordsAsync(string userId)
        {
            using var dbContext = _contextFactory.CreateDbContext();

            await dbContext.UserGameLibraries.
                Where(x => x.UserId == userId)
                .ExecuteDeleteAsync();
        }
    }
}
