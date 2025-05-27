using Microsoft.AspNetCore.Http.Extensions;
using MVC.Data;

namespace MVC.Middleware
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, ApplicationDbContext db)
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString();
            var url = httpContext.Request.GetDisplayUrl();
            var log = new Request()
            {
                RequestDate = DateTime.Now,
                IP = ip ?? "Unknown",
                Url = url
            };
            db.Request.Add(log);
            db.SaveChanges();

            // Kód před předáním dalšímu middleware
            Console.WriteLine($"Request: {httpContext.Request.Method} {httpContext.Request.Path}");

            await _next(httpContext);

            // Kód po zpracování dalšího middleware
            Console.WriteLine($"Response: {httpContext.Response.StatusCode}");
        }
    }
    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
