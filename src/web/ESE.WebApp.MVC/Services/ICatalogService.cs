using ESE.WebApp.MVC.Models;

namespace ESE.WebApp.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProductsById(Guid id);
    }
}
