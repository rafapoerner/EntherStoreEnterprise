using Microsoft.AspNetCore.Authentication.Cookies;

namespace ESE.WebApp.MVC.Configuration
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            options.LoginPath = "/login";
            options.AccessDeniedPath = "/erro/403";
        });
        }

        public static void UseIdentityConfig(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

        }
    }
}
