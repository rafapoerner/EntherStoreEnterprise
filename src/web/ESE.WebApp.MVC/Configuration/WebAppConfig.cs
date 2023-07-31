using Microsoft.AspNetCore.Authentication.Cookies;

namespace ESE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static void AddMvcConfig(this IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public static void UseMvcConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
