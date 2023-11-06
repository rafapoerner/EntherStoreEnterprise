using FluentValidation.Results;

namespace ESE.Core.Messages.Integration
{
    public class ResponseMessage : Message
    {
        public ValidationResult ValidationResult { get; private set; }

        public ResponseMessage(ValidationResult validationResult)
        {
             ValidationResult = validationResult;
        }
    }
}
