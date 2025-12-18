using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TaskManagerServer.Infra.Database.Extensions;

public static class EntityEntryExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r => r.TargetEntry != null
                                  && r.TargetEntry.Metadata.IsOwned()
                                  && r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}