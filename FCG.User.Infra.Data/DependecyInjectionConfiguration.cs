using Microsoft.Extensions.DependencyInjection;
using FCG.User.Infra.Data.Messaging.Config;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using FCG.User.Infra.Data.Messaging;
using FCG.User.Domain.Interfaces.Messaging;

namespace FCG.User.Infra.Data
{
    public static class DependecyInjectionConfiguration
    {
        //RabbitMq
        public static IServiceCollection ConfigureRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                var factory = new ConnectionFactory
                {
                    HostName = settings.HostName,
                    Port = settings.Port,
                    VirtualHost = settings.VirtualHost,
                    UserName = settings.UserName,
                    Password = settings.Password,
                    ClientProvidedName = settings.ClientProvidedName,
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
                };

                if (settings.UseSsl)
                {
                    factory.Ssl = new SslOption
                    {
                        Enabled = true,
                        ServerName = settings.HostName
                    };
                }

                return factory;
            });

            services.AddTransient<IQueueConsumer, RabbitMqConsumer>();
            return services;
        }

        //Amazon SQS
        public static IServiceCollection ConfigureAmazonSQS(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSQS>();
            return services;
        }
    }
}
