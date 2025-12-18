using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerServer.Lib.Domain.Enum;

namespace TaskManagerServer.Lib.Core.Requests.V1;

public class UpdateTaskRequest
{
    [Required]
    public OperationState Status { get; set; }
}