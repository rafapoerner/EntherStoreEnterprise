using ESE.WebApp.MVC.Models;
using Refit;

namespace ESE.WebApp.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProductsById(Guid id);
    }

    public interface ICatalogServiceRefit
    {
        [Get("/catalog/products/")]
        Task<IEnumerable<ProductViewModel>> GetAllProducts();

        [Get("/catalog/products/{id}")]
        Task<ProductViewModel> GetProductsById(Guid id);
    }
}
