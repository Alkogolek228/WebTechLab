using Microsoft.AspNetCore.Http.Extensions;
using Serilog;

namespace Web_253502_Alkhovik.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);

            if (httpContext.Response.StatusCode < 200 || httpContext.Response.StatusCode >= 300)
            {
                Log.Error($"request {httpContext.Request.GetDisplayUrl()} returns {httpContext.Response.StatusCode}");
            }
        }
    }

    public static class RequestLogginMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}