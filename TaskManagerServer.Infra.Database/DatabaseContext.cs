using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TaskManagerServer.Infra.Database.Configuration;
using TaskManagerServer.Lib.App.DataContext;
using TaskManagerServer.Lib.Domain.Entities;

namespace TaskManagerServer.Infra.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options), IDataContext
{
    public const string DatabaseContextMigrationsHistoryTableName = "MigrationsHistory";
    public const string SchemaName = "public";
    public DbSet<TaskEntity> TaskEntities { get; set; }
    public DbSet<TaskType> TaskTypes { get; set; }

    public void SetModified<T>(T entity) where T : class
    {
        Set<T>().Entry(entity).State = EntityState.Modified;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TaskTypeConfiguration());

        SeedData(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskType>().HasData(
            new TaskType { Id = 1, Name = "Авто", CreatedBy = -1, UpdatedBy = -1, Created = new DateTime(2025, 12, 18), Updated = new DateTime(2025, 12, 18) },
            new TaskType { Id = 2, Name = "Авиа", CreatedBy = -1, UpdatedBy = -1, Created = new DateTime(2025, 12, 18), Updated = new DateTime(2025, 12, 18) },
            new TaskType { Id = 3, Name = "ЖД", CreatedBy = -1, UpdatedBy = -1, Created = new DateTime(2025, 12, 18), Updated = new DateTime(2025, 12, 18) },
            new TaskType { Id = 4, Name = "ТХ", CreatedBy = -1, UpdatedBy = -1, Created = new DateTime(2025, 12, 18), Updated = new DateTime(2025, 12, 18) }
        );
    }
}