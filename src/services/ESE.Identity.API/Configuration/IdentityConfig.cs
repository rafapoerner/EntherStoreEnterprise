using ESE.Identity.API.Data;
using ESE.Identity.API.Extensions;
using ESE.WebApi.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ESE.Identity.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDefaultIdentity<IdentityUser>()
                            .AddRoles<IdentityRole>()
                            .AddErrorDescriber<IdentityMensagensPortugues>()
                            .AddEntityFrameworkStores<ApplicationDbContext>()
                            .AddDefaultTokenProviders();
            
            services.AddJwtConfiguration(configuration);

            return services;
        }
    }
}
