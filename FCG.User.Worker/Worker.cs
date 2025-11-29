using FCG.User.Application.DTO;
using FCG.User.Application.DTO.Messaging;
using FCG.User.Domain.Interfaces.Messaging;
using FCG.User.Infra.Data.Messaging.Config;
using Microsoft.Extensions.Options;

namespace FCG.User.Worker;

public class Worker(
    ILogger<Worker> logger, 
    IOptions<QueuesOptions> queuesOptions, 
    IQueueConsumer consumer, 
    IMessageHandler<AddGameInLibraryDTO> messageHandler) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await consumer.StartAsync(queuesOptions.Value.UserGameLibraryAddedQueue, messageHandler, stoppingToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error on Worker: {ErrorMessage}", ex.Message);
        }
        finally
        {
            logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
        }
    }
}
