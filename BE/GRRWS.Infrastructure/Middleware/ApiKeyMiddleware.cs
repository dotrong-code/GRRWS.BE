using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration["API_KEY"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Nếu request không thuộc nhóm /api/secure thì bỏ qua kiểm tra API_KEY
            if (!context.Request.Path.StartsWithSegments("/api/secure"))
            {
                await _next(context);
                return;
            }

            // Nếu là /api/secure thì kiểm tra API_KEY như bình thường
            if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("API Key is missing");
                return;
            }

            if (!_apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }

            await _next(context);
        }
    }

}
