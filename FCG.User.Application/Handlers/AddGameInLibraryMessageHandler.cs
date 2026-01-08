using DnsClient.Internal;
using FCG.User.Application.DTO;
using FCG.User.Application.DTO.Messaging;
using FCG.User.Application.Services;
using FCG.User.Application.Services.Interfaces;
using FCG.User.Domain.Entities;
using FCG.User.Domain.Interfaces.Messaging;
using FCG.User.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.User.Application.Handlers
{
    public class AddGameInLibraryMessageHandler : IMessageHandler<AddGameInLibraryDTO>
    {
        private readonly ILogger<AddGameInLibraryMessageHandler> _logger;

        private readonly IDbContextFactory<FCGDbContext> _contextFactory;

        public AddGameInLibraryMessageHandler(
            ILogger<AddGameInLibraryMessageHandler> logger,
            IDbContextFactory<FCGDbContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public async Task HandleAsync(AddGameInLibraryDTO message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Processing AddGameInLibrary. UserId={UserId}, GamesId={GamesId}",
                message.UserId,
                string.Join(",", message.GamesId ?? Array.Empty<string>())
            );
            try
            {
                using var dbContext = _contextFactory.CreateDbContext();

                foreach (var gameId in message.GamesId ?? Array.Empty<string>())
                {
                    if (string.IsNullOrWhiteSpace(gameId))
                        continue;

                    var entity = new UserGameLibrary(
                        message.UserId.ToString(),
                        gameId
                    );

                    await dbContext.UserGameLibraries.AddAsync(entity, cancellationToken);
                }

                await dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Finished AddGameInLibrary for user {UserId}",
                    message.UserId
                );
                //return Task.CompletedTask;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error processing AddGameInLibrary for user {UserId}", message.UserId);
                throw; 
            }
            
        }

    }
}
