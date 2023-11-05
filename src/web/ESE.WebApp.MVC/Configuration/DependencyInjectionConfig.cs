using ESE.WebApp.MVC.Extensions;
using ESE.WebApp.MVC.Services;
using ESE.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace ESE.Identity.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticatedService, AutenticatedService>();

            services.AddHttpClient<ICatalogService, CatalogService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    //.AddTransientHttpErrorPolicy(
                    // p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
                    .AddPolicyHandler(PollyExtensions.WaitAndRetryPolicy())
                    .AddTransientHttpErrorPolicy(
                        p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            //services.AddHttpClient("Refit", options =>
            //{
            //    options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
            //})
            //.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //.AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>); 

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();
        }
    }

    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetryPolicy()
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Tentando pela {retryCount} vez!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                );
            return retry;
        }
    }
}
