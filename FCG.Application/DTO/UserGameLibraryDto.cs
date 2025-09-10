namespace FCG.Application.DTO

{
    public class UserGameLibraryDto
    {
        //public required int Id { get; set; }
        public required string UserId { get; set; }
        public required int GameId { get; set; }
        public required DateTime AddedAt { get; set; }
    }
}
