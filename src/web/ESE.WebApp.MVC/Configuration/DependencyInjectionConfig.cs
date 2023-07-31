using ESE.WebApp.MVC.Services;

namespace ESE.Identity.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticatedService, AutenticatedService>();
        }
    }
}
