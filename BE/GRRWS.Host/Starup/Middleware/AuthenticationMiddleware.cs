namespace GRRWS.Host.Starup.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated && context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new
                {
                    status = StatusCodes.Status401Unauthorized,
                    title = "Unauthorized",
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                    errors = new[] { new { code = "Unauthorized", description = "User is not authenticated.", type = "Unauthorized" } }
                });
                return;
            }

            await _next(context);
        }
    }
}
