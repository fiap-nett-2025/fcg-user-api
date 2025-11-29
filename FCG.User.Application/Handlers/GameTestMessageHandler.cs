using FCG.User.Application.DTO;
using FCG.User.Domain.Interfaces.Messaging;
using Microsoft.Extensions.Logging;

namespace FCG.User.Application.Handlers
{
    public class GameTestMessageHandler(ILogger<GameTestMessageHandler> logger) : IMessageHandler<GameDto>
    {
        public Task HandleAsync(GameDto message, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Received game message: {Game}", message);
            return Task.CompletedTask;
        }
    }
}
