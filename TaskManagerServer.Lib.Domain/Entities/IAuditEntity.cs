namespace TaskManagerServer.Lib.Domain.Entities;

public interface IAuditEntity
{
    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    DateTimeOffset Created { get; set; }
    DateTimeOffset Updated { get; set; }
}