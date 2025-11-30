using Newtonsoft.Json;

namespace FCG.User.Application.DTO.Messaging
{
    public class AddGameInLibraryDTO
    {
        public required Guid UserId { get; init; }
        public string[] GamesId { get; set; } = Array.Empty<string>();

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
