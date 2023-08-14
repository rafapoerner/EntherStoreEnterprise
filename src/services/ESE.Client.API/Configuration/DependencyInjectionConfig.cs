using ESE.Clients.API.Data;

namespace ESE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //services.AddScoped<>();
            services.AddScoped<ClientsContext>();
        }
    }
}
