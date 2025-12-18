using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerServer.Lib.Domain.Entities;

public class TaskType : IAuditEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    public string Name { get; set; } = null!;

    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Updated { get; set; }
    public DateTimeOffset? Deleted { get; set; }
    public virtual ICollection<TaskEntity> ShipmentPlans { get; set; } = new List<TaskEntity>();
}
