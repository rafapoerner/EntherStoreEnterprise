using ESE.WebApp.MVC.Extensions;
using ESE.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ESE.WebApp.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient,
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);

            _httpClient = httpClient;

        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync("/catalog/products/");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetProductsById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalog/products/{id}");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<ProductViewModel>(response);
        }
    }
}
