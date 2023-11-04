using ESE.Clients.API.Models;
using ESE.Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace ESE.Clients.API.Application.Commands
{
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
    {
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var client = new Client(message.Id, message.Name, message.Email, message.Cpf);

            var clientExists = await _clientRepository.GetByCpf(client.Cpf.Numero);

            if (clientExists != null)
            {
                AddError("Este CPF já está em uso");
                return ValidationResult;
            }

            _clientRepository.ToAdd(client);

            return await PersistData(_clientRepository.UnitOfWork);
        }
    }
}