using ESE.Catalog.API.Data;
using ESE.Catalog.API.Data.Respository;
using ESE.Catalog.API.Models;
using ESE.WebApi.Core.Identity;
using Microsoft.EntityFrameworkCore;

namespace ESE.Catalog.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        }

        public static void UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
