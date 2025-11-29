using FCG.User.Domain.Enums;
using Newtonsoft.Json;

namespace FCG.User.Application.DTO
{
    public class GameDto
    {
        public required string Id { get; set; }
        public decimal Price { get; set; }
        public required GameGenre[] Genres { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
