using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerServer.Lib.App.Extensions;
using TaskManagerServer.Lib.Domain.Entities;
using TaskManagerServer.Lib.Domain.Enum;

namespace TaskManagerServer.Infra.Database.Configuration;

public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("TaskEntities");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .IsRequired()
            .HasComment("Идентификатор записи");

        builder
            .Property(a => a.TaskTypeId)
            .HasComment("FK на таблицу TaskTypes");

        builder
            .Property(a => a.Status)
            .HasMaxLength(50)
            .HasDefaultValue(OperationState.InProcess)
            .HasConversion(v => v.FromEnum(),
                v => v.ToEnum<OperationState>())
            .HasComment("Статус операции");

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
