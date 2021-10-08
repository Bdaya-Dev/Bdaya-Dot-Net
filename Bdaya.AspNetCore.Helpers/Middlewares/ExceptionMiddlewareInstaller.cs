namespace Microsoft.AspNetCore.Builder;

public static class ExceptionMiddlewareInstaller
{
    public static IApplicationBuilder ConfigureExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
