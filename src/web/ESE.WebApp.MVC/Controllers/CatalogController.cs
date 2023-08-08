using ESE.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESE.WebApp.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAllProducts();

            return View(products);
        }

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<IActionResult> ProductDetails(Guid id)
        {
            var product = await _catalogService.GetProductsById(id);

            return View(product);
        }
    }
}