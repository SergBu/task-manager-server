using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagerServer.Infra.Database.Interceptors;
using TaskManagerServer.Infra.Database.Services;
using TaskManagerServer.Lib.App.DataContext;

namespace TaskManagerServer.Infra.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (connectionString is null)
        {
            throw new ArgumentException("Error. ConnectionString is not set");
        }

        if (hostEnvironment.IsDevelopment() || hostEnvironment.IsStaging())
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        services.AddSingleton<EntityAuditInterceptor>();

        services.AddDbContextPool<DatabaseContext>((provider, options) =>
        {
            if (hostEnvironment.IsDevelopment() || hostEnvironment.IsStaging())
            {
                options.EnableSensitiveDataLogging();
            }

            options.UseNpgsql(connectionString, x =>
            {
                x.MigrationsHistoryTable(DatabaseContext.DatabaseContextMigrationsHistoryTableName,
                    DatabaseContext.SchemaName);
                x.SetPostgresVersion(new Version(9, 6));
            });
            options.AddInterceptors(provider.GetRequiredService<EntityAuditInterceptor>());
        });

        services.AddHostedService<MigrationService>();

        services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DatabaseContext>());

        return services;
    }
}