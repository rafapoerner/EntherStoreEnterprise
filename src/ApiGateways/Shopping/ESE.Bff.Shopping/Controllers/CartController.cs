using ESE.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESE.Bff.Shopping.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        //private readonly IAspNetUser _user;
        //private readonly CartContext _context;

        //public CartController(IAspNetUser user, CartContext context)
        //{
        //    _user = user;
        //    _context = context;
        //}


        [HttpGet]
        [Route("shopping/cart")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet]
        [Route("shopping/cart-quantity")]
        public async Task<IActionResult> GetQuantityVart()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("shopping/cart/items")]
        public async Task<IActionResult> AddItemCart()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("shopping/cart/items/{productId}")]
        public async Task<IActionResult> PutItemCart()
        {

            return CustomResponse();
        }

        [HttpDelete]
        [Route("shopping/cart/items/{productId}")]
        public async Task<IActionResult> DeleteItemCart()
        {
            return CustomResponse();
        }

    }
}