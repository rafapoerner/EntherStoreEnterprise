using ESE.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Mensagens.Any())
            {
                foreach (var message in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }

            return false;

        }

        protected void AddErrorValidate(string message)
        {
            ModelState.AddModelError(message, string.Empty);
        }

        protected bool OperationValid()
        {
            return ModelState.ErrorCount == 0;
        }

    }
}
