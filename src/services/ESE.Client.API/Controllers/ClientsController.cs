using ESE.Clients.API.Application.Commands;
using ESE.Core.Mediator;
using ESE.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ESE.Clients.API.Controllers
{
    public class ClientsController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientsController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.SendCommand(new RegisterClientCommand(Guid.NewGuid(), "Rafa", "rafa@rafa.com", "05346035729"));

            return CustomResponse(result);
        }
    }
}
