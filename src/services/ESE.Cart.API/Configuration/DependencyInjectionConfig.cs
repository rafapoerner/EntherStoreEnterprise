using ESE.Cart.API.Data;
using ESE.Cart.API.Model;
using ESE.WebApi.Core.User;

namespace ESE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<CartContext>();
        }
    }
}
