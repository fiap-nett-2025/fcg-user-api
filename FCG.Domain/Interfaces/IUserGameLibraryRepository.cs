using FCG.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Interfaces
{
    public interface IUserGameLibraryRepository
    {
        Task<IReadOnlyList<UserGameLibrary>> GetGamesLibraryByUserIdAsync(string userId);
        Task<UserGameLibrary?> GetOneGameFromUserLibraryAsync(string userId, int gameId);
        Task AddGameToUserLibraryAsync(UserGameLibrary userGameLibrary);
        Task UpdateGameInUserLibraryAsync(UserGameLibrary userGameLibrary);
        Task RemoveGameFromUserLibraryAsync(string userId, int gameId);
        Task RemoveAllRecordsAsync(string userId);
    }
}
