using System.Buffers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using TaskManagerServer.Lib.Domain.Enum;

namespace TaskManagerServer.Lib.Domain.Entities;

public class TaskEntity : IAuditEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int TaskTypeId { get; set; }

    [Required]
    public OperationState Status { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public DateTimeOffset? Deleted { get; set; }
    public virtual TaskType TaskType { get; set; } = null!;
}