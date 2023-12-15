using ESE.Cart.API.Model;

namespace ESE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<CartClient>();
        }
    }
}
