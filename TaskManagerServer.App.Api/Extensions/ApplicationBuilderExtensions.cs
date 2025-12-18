namespace TaskManagerServer.App.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
    {
        return app;
    }
}