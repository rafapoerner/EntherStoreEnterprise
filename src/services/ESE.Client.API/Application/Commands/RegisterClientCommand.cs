using ESE.Core.Messages;
using FluentValidation;

namespace ESE.Clients.API.Application.Commands
{
    public class RegisterClientCommand : Command
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public RegisterClientCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegisterClientValidation : AbstractValidator<RegisterClientCommand>
        {
            public RegisterClientValidation()
            {
                RuleFor(c => c.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido.");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do cliente não foi informado.");

                RuleFor(c => c.Cpf)
                    .Must(HasCpfValid)
                    .WithMessage("O CPF informado não é inválido.");

                RuleFor(c => c.Email)
                    .Must(HasEmailValid)
                    .WithMessage("O e-mail informado não é inválido.");
            }

            protected static bool HasCpfValid(string cpf)
            {
                return Core.DomainObjects.Cpf.Validate(cpf);
            }

            protected static bool HasEmailValid(string email)
            {
                return Core.DomainObjects.Email.Validate(email);
            }
        }
    }
}
