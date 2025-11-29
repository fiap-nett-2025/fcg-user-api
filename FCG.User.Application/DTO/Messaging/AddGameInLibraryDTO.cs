using Newtonsoft.Json;

namespace FCG.User.Application.DTO.Messaging
{
    public class AddGameInLibraryDTO
    {
        public required Guid UserId { get; init; }
        public required string[] GamesId { get; init; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
