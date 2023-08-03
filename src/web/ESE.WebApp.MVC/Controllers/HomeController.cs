using ESE.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if(id == 500)
            {
                modelError.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou entre em contato com nosso suporte.";
                modelError.Titulo = "Ocorreu um erro!";
                modelError.ErroCode = id;
            }
            else if(id == 404)
            {
                modelError.Mensagem = "A página que está procurando não existe! <br /> Em caso de dúvida, entre em contato com nosso suporte.";
                modelError.Titulo = "Ops! página não encontrada.";
                modelError.ErroCode = id;
            }
            else if (id == 403)
            {
                modelError.Mensagem = "Você não tem permissão para fazer isto.";
                modelError.Titulo = "Acesso negado.";
                modelError.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }
            return View("Error", modelError);
        }
    }
}