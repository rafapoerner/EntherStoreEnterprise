using ESE.Cart.API.Data;
using ESE.Cart.API.Model;
using ESE.WebApi.Core.Controllers;
using ESE.WebApi.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESE.Cart.API.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _context;

        public CartController(IAspNetUser user, CartContext context)
        {
            _user = user;
            _context = context;
        }


        [HttpGet("cart")]
        public async Task<CartClient> GetCart()
        {
            return await GetCartClient() ?? new CartClient();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddItemcart(CartItem item)
        {
            var cart = await GetCartClient();

            if(cart == null) 
                ManipulateNewCar(item);
            else
                ManipulateCartExists(cart, item);

            if(!ValidOperation()) return CustomResponse();

            var result = await _context.SaveChangesAsync();
            if (result > 0) AddProcessingError("Não foi possível persistir os dados no banco");

            return CustomResponse();
        }

        private void ManipulateNewCar(CartItem item)
        {
            var cart = new CartClient(_user.GetUserId());
            cart.AddItem(item);

            _context.CartClient.Add(cart);
        }

        private void ManipulateCartExists(CartClient cart, CartItem item) 
        {
            var productItemExist = cart.CartItemExist(item);

            cart.AddItem(item);

            if(productItemExist)
            {
                _context.CartItem.Update(cart.GetByProductId(item.ProductId));
            }
            else
            {
                _context.CartItem.Add(item);
            }

            _context.CartClient.Update(cart);
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

        private async Task<CartClient> GetCartClient()
        {
            return await _context.CartClient
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ClientId == _user.GetUserId());
        }


    }
}