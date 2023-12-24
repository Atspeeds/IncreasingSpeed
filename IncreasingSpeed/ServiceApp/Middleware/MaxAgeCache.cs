using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IncreasingSpeed.ServiceApp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MaxAgeCache
    {
        private readonly RequestDelegate _next;

        public MaxAgeCache(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            int maxAge = 5;
            httpContext.Request.Headers.Add("Cache-Control", $"publick,max-age={maxAge}");
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MaxAgeCacheExtensions
    {
        public static IApplicationBuilder UseMaxAgeCache(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MaxAgeCache>();
        }
    }
}
