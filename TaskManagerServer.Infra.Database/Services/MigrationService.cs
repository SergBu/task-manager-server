using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TaskManagerServer.Infra.Database.Services;

[ExcludeFromCodeCoverage]
public class MigrationService(ILogger<MigrationService> logger, IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Применение миграций базы данных");
            using var serviceScope = serviceProvider.CreateScope();

            await using var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

            await ApplyMigrations(context, stoppingToken);
            logger.LogInformation("Применение миграций базы данных завершено");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Возникли ошибки в процессе применения миграции базы данных. {ExMessage}", ex.Message);
            throw;
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }

    private async Task ApplyMigrations(DbContext context, CancellationToken cancellationToken = default)
    {
        context.Database.SetCommandTimeout(600);
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        await context.Database.MigrateAsync(cancellationToken: cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}