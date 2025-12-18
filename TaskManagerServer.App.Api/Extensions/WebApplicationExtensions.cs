using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace TaskManagerServer.App.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseSwaggerWithVersioning(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment() && !app.Environment.IsStaging()) return app;

        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var groupName in apiVersionDescriptionProvider.ApiVersionDescriptions.Select(s => s.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json",
                    $"API docs {groupName.ToUpperInvariant()}");
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
            }
            options.RoutePrefix = string.Empty;
            options.EnableDeepLinking();
        });

        return app;
    }
}