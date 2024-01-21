using ESE.Bff.Shopping.Extensions;
using Microsoft.Extensions.Options;

namespace ESE.Bff.Shopping.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient,
                                   IOptions<AppServicesSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);

            _httpClient = httpClient;

        }
    }
}
