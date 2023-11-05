using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace ESE.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]> // 1ª string é o nome da coleção(Mensagens) e a 2ª é a coleção em si(Erros.ToArray()).
            {
                { "Mensagens", Erros.ToArray() }
            }));

        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                AddProcessingError(error.ErrorMessage);
            }
            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AddProcessingError(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool ValidOperation()
        {
            return !Erros.Any();
        }

        protected void AddProcessingError(string erro)
        {
            Erros.Add(erro);
        }

        protected void ClearProcessingError()
        {
            Erros.Clear();
        }
    }
}
