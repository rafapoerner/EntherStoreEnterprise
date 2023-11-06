using FluentValidation.Results;
using MediatR;
using ESE.Clients.API.Application.Commands;
using ESE.Clients.API.Application.Events;
using ESE.Clients.API.Data.Repository;
using ESE.Clients.API.Models;
using ESE.Core.Mediator;
using ESE.Clients.API.Data;
using ESE.Clients.API.Services;

namespace ESE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();

            services.AddScoped<INotificationHandler<ClientRegistratedEvent>, ClientEventHandler>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ClientsContext>();

            services.AddHostedService<RegisterClientIntegrationHandler>();
        }
    }
}
