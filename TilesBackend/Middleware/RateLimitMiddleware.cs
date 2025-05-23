using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace TilesBackendApI.Middleware
{

    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly int _maxRequests;
        private readonly TimeSpan _window;
        private readonly string _limitMessage;

        public RateLimitMiddleware(RequestDelegate next, IMemoryCache cache, int maxRequests, TimeSpan window, string limitMessage)
        {
            _next = next;
            _cache = cache;
            _maxRequests = maxRequests;
            _window = window;
            _limitMessage = limitMessage;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            var cacheKey = $"RateLimit-{ipAddress}-{context.Request.Path}";

            var entry = _cache.GetOrCreate(cacheKey, cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = _window;
                return new RequestCounter { Count = 0 };
            });

            if (entry.Count >= _maxRequests)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests; // 429
                await context.Response.WriteAsync(_limitMessage);
                return;
            }

            entry.Count++;
            _cache.Set(cacheKey, entry, _window);

            await _next(context);
        }

        private class RequestCounter
        {
            public int Count { get; set; }
        }
    }

    // Extension method for easy middleware registration
    public static class RateLimitMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomRateLimit(this IApplicationBuilder builder,
            int maxRequests, TimeSpan window, string limitMessage)
        {
            return builder.UseMiddleware<RateLimitMiddleware>(maxRequests, window, limitMessage);
        }
    }
}


