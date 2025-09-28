namespace FCG.User.Domain.Entities;

public class UserGameLibrary
{
	//public int Id { get; set; }
	public string UserId { get; set; }
	public string GameId { get; set; }
    public DateTime AddedAt { get; private set; } = DateTime.UtcNow;
    public User User { get; set; } = default!;
    public UserGameLibrary(string userId, string gameId)
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

    public static void ValidateGameId(string gameId)
    {
        if (string.IsNullOrWhiteSpace(gameId))
            throw new ArgumentException("GameId não deve ser vazio.");
    }

    public void UpdateUserId(string userId)
    {
        ValidateUserId(userId);
        UserId = userId;
    }

    public void UpdateGameId(string gameId)
    {
        ValidateGameId(gameId);
        GameId = gameId;
    }
}
