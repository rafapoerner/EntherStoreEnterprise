using ESE.Catalog.API.Models;
using ESE.WebApi.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESE.Catalog.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalog/products")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productRepository.GetAll();
        }


        [ClaimsAuthorize("Catalog", "Read")]
        [HttpGet("catalog/products/{id}")]
        public async Task<Product> ProductDetail(Guid id)
        {
            //throw new Exception("Erro!");
            return await _productRepository.GetById(id);
        }
    }
}
