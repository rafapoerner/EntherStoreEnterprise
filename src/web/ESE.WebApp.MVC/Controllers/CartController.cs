using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ESE.WebApp.MVC.Models;
using ESE.WebApp.MVC.Services;

namespace ESE.WebApp.MVC.Controllers
{
    [Authorize]
    public class CartController : MainController
    {
        private readonly ICartService _cartService;
        private readonly ICatalogService _catalogService;

        public CartController(ICatalogService catalogService, ICartService cartService)
        {
            _catalogService = catalogService;
            _cartService = cartService;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddItemCart(ItemProductViewModel itemProduct)
        {
            var product = await _catalogService.GetProductsById(itemProduct.ProductId);

            ValidateItemCart(product, itemProduct.Quantidade);

            if (!OperationValid()) return View("Index", await _cartService.GetCart());

            itemProduct.Name = product.Name;
            itemProduct.Price = product.Price;
            itemProduct.Image = product.Image;

            var response = await _cartService.AddItemCart(itemProduct);

            if (ResponseHasErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, int quantidade)
        {

            var product = await _catalogService.GetProductsById(productId);

            ValidateItemCart(product, quantidade);
            if (!OperationValid()) return View("Index", await _cartService.GetCart());


            var itemProduto = new ItemProductViewModel { ProductId = productId, Quantidade = quantidade };
            var response = await _cartService.UpdateItemCart(productId, itemProduto);

            if (ResponseHasErrors(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {
            var product = await _catalogService.GetProductsById(productId);

            if(product == null)
            {
                AddErrorValidate("Produto inexistente!");
                return View("Index", await _cartService.GetCart());
            }

            var response = await _cartService.RemoveItemCart(productId);

            if(ResponseHasErrors(response)) return View("Index", await _cartService.GetCart()); 

            return RedirectToAction("Index");
        }

        private void ValidateItemCart(ProductViewModel product, int quantidade )
        {

            if (product == null) AddErrorValidate("Produto Inexistente!");
            if (quantidade < 1) AddErrorValidate($"Escolha ao menos uma unidade do produto {product.Name}");
            if (quantidade > product.QuantityStock) AddErrorValidate($"O produto {product.Name} possui {product.QuantityStock} undades em estoque e você selecionou {quantidade}");
            
        }
    }
}
