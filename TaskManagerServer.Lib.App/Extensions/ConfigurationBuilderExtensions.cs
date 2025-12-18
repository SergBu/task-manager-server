using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TaskManagerServer.Lib.App.Extensions;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder ConfigureConfiguration(this IConfigurationBuilder builder, IHostEnvironment environment, string[] args)
    {
        builder
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile(Path.Combine("Configuration", environment.EnvironmentName, "appsettings.json"), true, false)
            .AddJsonFile(Path.Combine("Configuration", "nlog.json"), false, false)
            .AddJsonFile(Path.Combine("Configuration", environment.EnvironmentName, "secrets.json"), true, false)
            ;

        return builder
            .AddEnvironmentVariables()
            .AddCommandLine(args);
    }
}