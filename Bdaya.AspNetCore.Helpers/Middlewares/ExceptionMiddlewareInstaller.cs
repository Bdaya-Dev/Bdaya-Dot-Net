namespace Microsoft.AspNetCore.Builder;

public static class ExceptionMiddlewareInstaller
{
    public static IApplicationBuilder UseBdayaExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
