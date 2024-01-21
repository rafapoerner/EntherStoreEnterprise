using ESE.WebApi.Core.User;
using Polly;
using Polly.Retry;

namespace ESE.Bff.Shopping.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            #region HttpServices
            //services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            //services.AddHttpClient<IAutenticatedService, AutenticatedService>()
            //        .AddPolicyHandler(PollyExtensions.WaitAndRetryPolicy())
            //        .AddTransientHttpErrorPolicy(
            //            p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            //services.AddHttpClient<ICatalogService, CatalogService>()
            //        .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //        .AddPolicyHandler(PollyExtensions.WaitAndRetryPolicy())
            //        .AddTransientHttpErrorPolicy(
            //            p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            //services.AddHttpClient<ICartService, CartService>()
            //        .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //        .AddPolicyHandler(PollyExtensions.WaitAndRetryPolicy())
            //        .AddTransientHttpErrorPolicy(
            //            p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            #endregion

        }
    }

    #region PollyExtensions
    //public class PollyExtensions
    //{
    //    public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetryPolicy()
    //    {
    //        var retry = HttpPolicyExtensions
    //            .HandleTransientHttpError()
    //            .WaitAndRetryAsync(new[]
    //            {
    //                TimeSpan.FromSeconds(1),
    //                TimeSpan.FromSeconds(5),
    //                TimeSpan.FromSeconds(10),
    //            }, (outcome, timespan, retryCount, context) =>
    //            {
    //                Console.ForegroundColor = ConsoleColor.Red;
    //                Console.WriteLine($"Tentando pela {retryCount} vez!");
    //                Console.ForegroundColor = ConsoleColor.White;
    //            }
    //            );
    //        return retry;
    //    }
    //}
    #endregion
}
