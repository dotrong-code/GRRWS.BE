using GRRWS.Host.Starup.Middleware;

namespace GRRWS.Host.Starup
{
    public static class MiddlewareConfiguration
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            return app;
        }
    }
}
