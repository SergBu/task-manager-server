using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerServer.Infra.Database.Extensions;
using TaskManagerServer.Lib.Core.Interfaces;
using TaskManagerServer.Lib.Domain.Entities;

namespace TaskManagerServer.Infra.Database.Interceptors;

public class EntityAuditInterceptor(IServiceProvider serviceProvider) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        var currentUserService = serviceProvider.GetRequiredService<ICurrentUserService>();
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<IAuditEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.Created = DateTimeOffset.UtcNow;

            if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                entry.Entity.Updated = DateTimeOffset.UtcNow;
        }

        foreach (var entry in context.ChangeTracker.Entries<IAuditEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedBy = currentUserService.UserId;

            if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                entry.Entity.UpdatedBy = currentUserService.UserId;
        }
    }
}