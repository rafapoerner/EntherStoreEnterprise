using EasyNetQ;
using ESE.Clients.API.Application.Commands;
using ESE.Core.Mediator;
using ESE.Core.Messages.Integration;
using FluentValidation.Results;

namespace ESE.Clients.API.Services
{
    public class RegisterClientIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterClientIntegrationHandler(IServiceProvider serviceProvider)
        {
          _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost"); ;

            _bus.Rpc.RespondAsync<UserRegistratedIntegrationEvent, ResponseMessage>(async request =>
            new ResponseMessage(await RegisterClient(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegisterClient(UserRegistratedIntegrationEvent message)
        {
            var clientCommand = new RegisterClientCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult success;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.SendCommand(clientCommand);
            }

            return success;
        }
    }
}
