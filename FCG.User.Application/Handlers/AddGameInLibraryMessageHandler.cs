using FCG.User.Application.DTO;
using FCG.User.Application.DTO.Messaging;
using FCG.User.Domain.Interfaces.Messaging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.User.Application.Handlers
{
    public class AddGameInLibraryMessageHandler(ILogger<AddGameInLibraryMessageHandler> logger) : IMessageHandler<AddGameInLibraryDTO>
    {
        public Task HandleAsync(AddGameInLibraryDTO message, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Received game library: {Game}", message);
            return Task.CompletedTask;
        }
    }
}
