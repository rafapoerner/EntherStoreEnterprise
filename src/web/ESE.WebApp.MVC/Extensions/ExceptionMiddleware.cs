using Microsoft.AspNetCore.Authentication;
using Polly.CircuitBreaker;
using Refit;
using System.Net;

namespace ESE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {

                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch(ValidationApiException  ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ApiException ex) 
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (BrokenCircuitException) 
            {
                HandleCircuitBreakerExceptionAsync(httpContext);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCodes) 
        {
            if(statusCodes == HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}"); // O Redirect não interrompe o processo, por isso a necessidade do return.
                return;
            }
            context.Response.StatusCode = (int)statusCodes;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context) 
        {
            context.Response.Redirect("/sistema-indisponivel");
        }
    }
}
