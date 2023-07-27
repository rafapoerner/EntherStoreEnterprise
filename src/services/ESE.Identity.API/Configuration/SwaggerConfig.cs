using Microsoft.OpenApi.Models;

namespace ESE.Identity.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Enther Store Enterprise",
                    Description = "Esta API foi desenvolvida com base no curso de Eduardo Pires ASP.NET Core Enterprise Applications",
                    Contact = new OpenApiContact() { Name = "Rafa Poerner", Email = "rafa_poerner@hotmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/lincenses/MIT") }
                });
            });

            return services;

        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
