using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ESE.Clients.API.Application.Commands;
using ESE.Clients.API.Data;
using ESE.Core.Mediator;
using ESE.Clients.API.Models;

namespace ESE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();

            services.AddScoped<IClientRepository, IClientRepository>();
            services.AddScoped<ClientsContext>();
        }
    }
}
