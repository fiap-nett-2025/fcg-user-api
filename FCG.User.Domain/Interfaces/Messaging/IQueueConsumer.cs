using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.User.Domain.Interfaces.Messaging
{
    public interface IQueueConsumer
    {
        Task StartAsync<T>(string queueName, IMessageHandler<T> handler,
            CancellationToken cancellationToken = default);
    }
}
