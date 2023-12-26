
using ESE.WebApp.MVC.Extensions;
using ESE.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ESE.WebApp.MVC.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }

        public async Task<CartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            HandleResponseErrors(response);

            return await DeserializeObjectResponse<CartViewModel>(response);

        }

        public async Task<ResponseResult> AddItemCart(ItemProductViewModel Product)
        {
            var itemContent = GetContent(Product);

            var response = await _httpClient.PostAsync("/cart/", itemContent);

            if(!HandleResponseErrors(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return new ResponseResult();
        }

        public async Task<ResponseResult> UpdateItemCart(Guid productId, ItemProductViewModel product)
        {
            var itemContent = GetContent(product);

            var response = await _httpClient.PutAsync($"/cart/{product.ProductId}", itemContent);

            if (!HandleResponseErrors(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return new ResponseResult();
        }

        public async Task<ResponseResult> RemoveItemCart(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");

            if (!HandleResponseErrors(response)) return await DeserializeObjectResponse<ResponseResult>(response);

            return new ResponseResult();
        }

    }
}
