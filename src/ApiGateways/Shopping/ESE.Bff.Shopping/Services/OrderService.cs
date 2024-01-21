
using ESE.Bff.Shopping.Extensions;
using Microsoft.Extensions.Options;

namespace ESE.Bff.Shopping.Services
{
    public class OrderService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CartUrl);
        }
    }
}
