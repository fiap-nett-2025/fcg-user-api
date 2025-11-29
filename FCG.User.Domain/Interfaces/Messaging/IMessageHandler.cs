using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.User.Domain.Interfaces.Messaging
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message, CancellationToken cancellationToken = default);
    }
}
