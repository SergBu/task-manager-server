using Microsoft.Extensions.Hosting.Internal;
using NLog;
using NLog.Web;
using TaskManagerServer.App.Api.Extensions;
using TaskManagerServer.Infra.Database.Extensions;
using TaskManagerServer.Infra.Logging.Extensions;
using TaskManagerServer.Lib.App.Extensions;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;
    var builder = TaskManagerServer.App.Api.Program.CreateApplicationBuilder(args, environment);

    var app = builder.Build();
    app.UseHealthChecks();
    app.UseRouting();

    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSwaggerWithVersioning();

    //app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Fatal(exception, $"Stopped program because of exception: {exception.Message}");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}
namespace TaskManagerServer.App.Api
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class Program
    {
        public static WebApplicationBuilder CreateApplicationBuilder(string[] args, string environment)
        {
            var rootPath = Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? "";
            var hostingEnvironment = new HostingEnvironment
            {
                EnvironmentName = environment,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                ApplicationName = AppDomain.CurrentDomain.FriendlyName
            };
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(rootPath)
                .ConfigureConfiguration(hostingEnvironment, args)
                .Build();

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.AddLogger(configurationBuilder);
            builder.Configuration.AddConfiguration(configurationBuilder);

            builder.Services.AddAuthorization(builder.Configuration, builder.Environment);
            builder.Services.AddDatabase(builder.Configuration, builder.Environment);
            
            builder.Services.AddCore(builder.Configuration);
            builder.Services.AddHttpClient();
            builder.Services.AddFilters();
            builder.Services.AddValidation();
            builder.Services.ConfigureRouting();
            builder.Services.AddSwaggerWithVersioning(builder.Environment);
            
            return builder;
        }
    }
}