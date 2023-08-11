using ESE.WebApp.MVC.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace ESE.WebApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static void AddMvcConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.Configure<AppSettings>(configuration);
        }

        public static void UseMvcConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{

            //}

            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfig();

            var supportedCulture = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCulture,
                SupportedUICultures = supportedCulture,
            });

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalog}/{action=Index}/{id?}");
            });
        }
    }
}
