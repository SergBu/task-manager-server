using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerServer.Lib.Domain.Enum;

namespace TaskManagerServer.Lib.Core.Responses.V1;

public class GetTaskResponse
{
    public int Id { get; set; }
    public int TaskTypeId { get; set; }
    public OperationState Status { get; set; } 
    public bool IsDeleted { get; set; }
}
