using ESE.Cart.API.Model;
using ESE.WebApi.Core.Controllers;
using ESE.WebApi.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESE.Cart.API.Controllers
{




    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;

        public CartController(IAspNetUser user)
        {
            _user = user;
        }


        [HttpGet("cart")]
        public async Task<CartClient> GetCart()
        {
            return null;
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddItemcart(CartItem item)
        {
            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, CartItem item)
        {
            return CustomResponse();
        }

        public async Task<IActionResult> DeleteItemCart(Guid productId)
        {
            return CustomResponse();
        }

    }
}