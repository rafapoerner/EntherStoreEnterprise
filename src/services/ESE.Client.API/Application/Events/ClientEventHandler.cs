using MediatR;

namespace ESE.Clients.API.Application.Events
{
    public class ClientEventHandler : INotificationHandler<ClientRegistratedEvent>
    {
        public Task Handle(ClientRegistratedEvent notification, CancellationToken cancellationToken)
        {
            //Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}
