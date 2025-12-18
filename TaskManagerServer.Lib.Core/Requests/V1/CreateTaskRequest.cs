using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerServer.Lib.Core.Requests.V1;

public class CreateTaskRequest
{
    [Required]
    public int TaskTypeId { get; set; } 
}
