using FCG.User.Application.DTO;
using FCG.User.Application.Services.Interfaces;
using FCG.User.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.User.API.Controllers;

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

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAllUserLibraryGames(string userId)
    {
        var userGameLibrary = await _userGameLibraryServices.GetAllGamesFromUserLibraryAsync(userId);

        return Success(userGameLibrary, "Biblioteca de jogos do usuario retornada com sucesso");
    }

    [HttpGet("{userId}/game/{gameId}")]
    public async Task<IActionResult> GetOneGameFromUserLibrary(string userId, string gameId)
    {
        var item = await _userGameLibraryServices.GetOneGameFromUserLibraryAsync(userId, gameId);

        return Success(item, "Jogo presente na biblioteca do usuário:");
    }

    [HttpPost("{userId}/game/{gameId}")]
    public async Task<IActionResult> AddGameToUserLibrary(string userId, string gameId)
    {
        var item = await _userGameLibraryServices.AddGameToUserLibraryAsync(userId, gameId);
        return CreatedResponse(item, "Jogo adcionado na biblioteca com sucesso.");
    }

    [HttpDelete("{userId}/game/{gameId}")]
    public async Task<IActionResult> RemoveGameFromUserLibrary(string userId, string gameId)
    {
        await _userGameLibraryServices.DeleteGameInUserLibraryAsync(userId, gameId);
        return NoContent();
    }
}
