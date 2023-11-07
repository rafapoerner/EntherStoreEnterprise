using ESE.Clients.API.Services;
using ESE.Core.Utils;
using ESE.MessageBus;

namespace ESE.Clients.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                    .AddHostedService<RegisterClientIntegrationHandler>();
        }
    }
}
