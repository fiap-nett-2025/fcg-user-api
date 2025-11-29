using FCG.User.Application.DTO;
using FCG.User.Application.DTO.Messaging;
using FCG.User.Application.Handlers;
using FCG.User.Domain.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.User.Application
{
    public static class DependecyInjectionConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Handlers
            services.AddTransient<IMessageHandler<AddGameInLibraryDTO>, AddGameInLibraryMessageHandler>();
            return services;
        }
    }
}
