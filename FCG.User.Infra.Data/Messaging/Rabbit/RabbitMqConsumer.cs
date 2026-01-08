using FCG.User.Domain.Interfaces.Messaging;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FCG.User.Infra.Data.Messaging.Rabbit
{
    public class RabbitMqConsumer(ConnectionFactory factory, ILogger<RabbitMqConsumer> logger) : IQueueConsumer
    {
        public async Task StartAsync<T>(string queueName, IMessageHandler<T> handler,
            CancellationToken cancellationToken = default)
        {
            using IConnection connection = await factory.CreateConnectionAsync(cancellationToken);
            using IChannel channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken
            );

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                string json = string.Empty;
                try
                {
                    var body = ea.Body.ToArray();
                    json = Encoding.UTF8.GetString(body);
                    var message = JsonConvert.DeserializeObject<T>(json)
                        ?? throw new InvalidOperationException("Failed to deserialize message");

                    await handler.HandleAsync(message, cancellationToken);
                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error on deserializing json to type {Type} on queue '{QueueName}': {Json}", typeof(T), queueName, json);
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
                }
            };

            await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer, cancellationToken: cancellationToken);

            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
    }
}
