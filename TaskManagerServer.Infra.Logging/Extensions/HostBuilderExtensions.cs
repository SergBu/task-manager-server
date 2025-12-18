using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace TaskManagerServer.Infra.Logging.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddLogger(this IHostBuilder builder, IConfiguration configuration)
    {
        builder.ConfigureLogging((_, logging) =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
            logging.AddNLog(new NLogLoggingConfiguration(configuration.GetSection("NLog")), new NLogProviderOptions
            {
                IncludeScopes = true,
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true,
                ParseMessageTemplates = true
            });
        });

        builder.ConfigureServices((context, services) =>
        {
            services.AddScoped<ILogger>(provider => provider.GetRequiredService<ILogger<HostBuilder>>());
        });

        return builder;
    }
}