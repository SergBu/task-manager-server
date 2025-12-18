using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerServer.Lib.Domain.Entities;

namespace TaskManagerServer.Infra.Database.Configuration;

public class TaskTypeConfiguration : IEntityTypeConfiguration<TaskType>
{
    public void Configure(EntityTypeBuilder<TaskType> builder)
    {
        builder.ToTable("TaskTypes");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .IsRequired()
            .HasComment("Идентификатор записи");

        builder
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasComment("Название типа");

        builder
            .Property(a => a.Created)
            .HasComment("Время создания записи");

        builder
            .Property(a => a.Updated)
            .HasComment("Время последнего изменения записи");

        builder
            .Property(a => a.Deleted)
            .HasComment("Дата и время удаления записи");

        builder
            .Property(a => a.CreatedBy)
            .HasComment("Id пользователя создавшего запись");

        builder
            .Property(a => a.UpdatedBy)
            .HasComment("Id пользователя внесшего последние изменения");
    }
}
