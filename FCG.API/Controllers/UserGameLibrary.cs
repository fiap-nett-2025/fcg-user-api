using FCG.Application.DTO;
using FCG.Application.Services.Interfaces;
using FCG.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserGameLibrary : ApiBaseController
{
    private readonly IUserGameLibraryServices _userGameLibraryServices;

    public UserGameLibrary(IUserGameLibraryServices userGameLibraryServices)
	{
        _userGameLibraryServices = userGameLibraryServices;
	}

    [HttpGet("get-all-games-from-user-library/{userId}")]
    public async Task<IActionResult> GetAllUserLibraryGames(string userId)
    {
        var userGameLibrary = await _userGameLibraryServices.GetAllGamesFromUserLibraryAsync(userId);

        return Success(userGameLibrary, "Biblioteca de jogos do usuario retornada com sucesso");
    }

    [HttpGet("get-specific-game-in-user-library/{userId}/game/{gameId:int}")]
    public async Task<IActionResult> GetOneGameFromUserLibrary(string userId, int gameId)
    {
        var item = await _userGameLibraryServices.GetOneGameFromUserLibraryAsync(userId, gameId);

        return Success(item, "Jogo presente na biblioteca do usuário:");
    }

    [HttpPost("add-game-to-user-library")]
    public async Task<IActionResult> AddGameToUserLibrary(UpsertGameToUserLibrary model)
    {
        var item = await _userGameLibraryServices.AddGameToUserLibraryAsync(model);
        return CreatedResponse(item, "Jogo adcionado na biblioteca com sucesso.");
    }

    [HttpDelete("remove-game-from-user-library/{userId}/game/{gameId:int}")]
    public async Task<IActionResult> RemoveGameFromUserLibrary(string userId, int gameId)
    {
        await _userGameLibraryServices.DeleteGameInUserLibraryAsync(userId, gameId);
        return NoContent();
    }

    [HttpDelete("remove-all-user-game-library-records/{userId}")]
    public async Task<IActionResult> RemoveAllUserGameLibraryRecords(string userId)
    {
        await _userGameLibraryServices.DeleteAllUserGameLibraryRecordsAsync(userId);
        return NoContent();
    }
}
