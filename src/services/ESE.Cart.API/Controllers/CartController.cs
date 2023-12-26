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

            await PersistingData();

            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, CartItem item)
        {
            var cart = await GetCartClient();
            var itemCart = await GetItemCartValid(productId, cart, item);
            if (itemCart == null) return CustomResponse();

            cart.UpdateUnities(itemCart, item.Quantidade);

            CartValidate(cart);
            if (!ValidOperation()) return CustomResponse();

            _context.CartItem.Update(itemCart);
            _context.CartClient.Update(cart);

            await PersistingData();

            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {
            var cart = await GetCartClient();

            var itemCart = await GetItemCartValid(productId, cart);
            if(itemCart == null) return CustomResponse();

            CartValidate(cart);
            if (!ValidOperation()) return CustomResponse();

            cart.RemoveItem(itemCart);

            _context.CartItem.Remove(itemCart);
            _context.CartClient.Update(cart);

            await PersistingData();
            return CustomResponse();
        }

        private async Task<CartClient> GetCartClient()
        {
            return await _context.CartClient
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ClientId == _user.GetUserId());
        }

        private void ManipulateNewCar(CartItem item)
        {
            var cart = new CartClient(_user.GetUserId());
            cart.AddItem(item);

            CartValidate(cart);
            _context.CartClient.Add(cart);
        }

        private void ManipulateCartExists(CartClient cart, CartItem item)
        {
            var productItemExist = cart.CartItemExist(item);

            cart.AddItem(item);
            CartValidate(cart);

            if (productItemExist)
            {
                _context.CartItem.Update(cart.GetByProductId(item.ProductId));
            }
            else
            {
                _context.CartItem.Add(item);
            }


            _context.CartClient.Update(cart);
        }

        private async Task<CartItem> GetItemCartValid(Guid productId, CartClient cart, CartItem item = null)
        {
            if (item != null && productId != item.ProductId)
            {
                AddProcessingError("O item não corresponde ao informado");
                return null;
            }

            if (cart == null)
            {
                AddProcessingError("Carrinho não encontrado");
            }

            var itemCart = await _context.CartItem
                .FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

            if (itemCart == null || !cart.CartItemExist(itemCart))
            {
                AddProcessingError("O item não está no carrinho");
                return null;
            }

            return itemCart;
        }

        private async Task PersistingData()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddProcessingError("Não foi possível persistir os dados no banco");
        }

        private bool CartValidate(CartClient cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
            return false;
        }

    }
}