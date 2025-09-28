using FCG.User.Application.DTO;
using FCG.User.Application.Services;

namespace FCG.User.Application.Services.Interfaces;

public interface IUserGameLibraryServices
{
    Task<IEnumerable<UserGameLibraryDto>> GetAllGamesFromUserLibraryAsync(string userId);
    Task<UserGameLibraryDto> GetOneGameFromUserLibraryAsync(string userId, string gameId);
    Task<UserGameLibraryDto> AddGameToUserLibraryAsync(string userId, string gameId);
    Task UpdateGameInUserLibraryAsync(UpsertGameToUserLibrary model);
    Task DeleteGameInUserLibraryAsync(string userId, string GameId);
    Task DeleteAllUserGameLibraryRecordsAsync(string userId);
}
