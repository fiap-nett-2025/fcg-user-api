using FCG.Application.DTO;
using FCG.Application.Services;

namespace FCG.Application.Services.Interfaces;

public interface IUserGameLibraryServices
{
    Task<IEnumerable<UserGameLibraryDto>> GetAllGamesFromUserLibraryAsync(string userId);
    Task<UserGameLibraryDto> GetOneGameFromUserLibraryAsync(string userId, int gameId);
    Task<UserGameLibraryDto> AddGameToUserLibraryAsync(UpsertGameToUserLibrary model);
    Task UpdateGameInUserLibraryAsync(UpsertGameToUserLibrary model);
    Task DeleteGameInUserLibraryAsync(string userId, int GameId);
    Task DeleteAllUserGameLibraryRecordsAsync(string userId);
}
