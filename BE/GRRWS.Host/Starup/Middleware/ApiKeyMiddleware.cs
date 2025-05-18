using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Common;

namespace GRRWS.Host.Starup.Middleware
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
            if (!context.Request.Path.StartsWithSegments("/api/secure"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey))
            {
                var result = Result.Failure(Error.Unauthorized(
                    code: "ApiKey.Missing",
                    description: "API Key is missing"
                    ));

                await result.ToProblemDetails().ExecuteAsync(context);
                return;
            }

            if (!_apiKey.Equals(extractedApiKey))
            {
                var result = Result.Failure(Error.Unauthorized(
                    code: "ApiKey.Invalid",
                    description: "Unauthorized client"));

                await result.ToProblemDetails().ExecuteAsync(context);
                return;
            }

            await _next(context);
        }
    }

}
