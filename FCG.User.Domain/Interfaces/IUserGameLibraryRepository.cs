using FCG.User.Domain.Entities;

namespace FCG.User.Domain.Interfaces
{
    public interface IUserGameLibraryRepository
    {
        Task<IReadOnlyList<UserGameLibrary>> GetGamesLibraryByUserIdAsync(string userId);
        Task<UserGameLibrary?> GetOneGameFromUserLibraryAsync(string userId, string gameId);
        Task AddGameToUserLibraryAsync(UserGameLibrary userGameLibrary);
        Task UpdateGameInUserLibraryAsync(UserGameLibrary userGameLibrary);
        Task RemoveGameFromUserLibraryAsync(string userId, string gameId);
        Task RemoveAllRecordsAsync(string userId);
    }
}
