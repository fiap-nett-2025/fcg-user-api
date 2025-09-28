namespace FCG.User.Application.DTO

{
    public class UserGameLibraryDto
    {
        //public required int Id { get; set; }
        public required string UserId { get; set; }
        public required string GameId { get; set; }
        public required DateTime AddedAt { get; set; }
    }
}
