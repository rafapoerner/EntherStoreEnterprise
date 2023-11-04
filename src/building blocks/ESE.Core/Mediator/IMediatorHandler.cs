using ESE.Core.Messages;
using FluentValidation.Results;

namespace ESE.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}

