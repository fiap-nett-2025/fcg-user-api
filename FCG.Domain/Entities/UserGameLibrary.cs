namespace FCG.Domain.Entities;

public class UserGameLibrary
{
	//public int Id { get; set; }
	public string UserId { get; set; }
	public int GameId { get; set; }
    public DateTime AddedAt { get; private set; } = DateTime.UtcNow;

    public UserGameLibrary(string userId, int gameId)
	{
        ValidateUserId(userId);
        ValidateGameId(gameId);

        UserId = userId;
		GameId = gameId;
	}

    private UserGameLibrary(){}

    public static void ValidateUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId não pode ser nulo, vazio ou espaços.", nameof(userId));
    }

    public static void ValidateGameId(int gameId)
    {
        if (gameId <= 0)
            throw new ArgumentException("GameId deve ser maior que zero.");
    }

    public void UpdateUserId(string userId)
    {
        ValidateUserId(userId);
        UserId = userId;
    }

    public void UpdateGameId(int gameId)
    {
        ValidateGameId(gameId);
        GameId = gameId;
    }
}
