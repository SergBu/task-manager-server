using Microsoft.EntityFrameworkCore;
using TaskManagerServer.Lib.Domain.Entities;

namespace TaskManagerServer.Lib.App.DataContext;

public interface IDataContext
{
    public DbSet<TaskEntity> TaskEntities { get; set; }
    public DbSet<TaskType> TaskTypes { get; set; }

    void SetModified<T>(T entity) where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}