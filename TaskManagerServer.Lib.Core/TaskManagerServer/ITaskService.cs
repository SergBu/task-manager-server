using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerServer.Lib.Core.Requests.V1;
using TaskManagerServer.Lib.Core.Responses.V1;

namespace TaskManagerServer.Lib.Core.TaskManagerServer;

public interface ITaskService
{
    public Task<List<GetTaskResponse>> GetAllAsync();
    public Task<GetTaskResponse> GetByIdAsync(int id);
    public Task DeleteAsync(int id);
    public Task CreateAsync(CreateTaskRequest request);
    public Task UpdateAsync(int id, UpdateTaskRequest request);
}
