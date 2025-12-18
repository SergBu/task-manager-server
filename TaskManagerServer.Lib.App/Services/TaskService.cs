using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerServer.Lib.Core.Interfaces;
using TaskManagerServer.Lib.Core.Requests.V1;
using TaskManagerServer.Lib.Core.Responses.V1;
using TaskManagerServer.Lib.Domain.Entities;
using TaskManagerServer.Lib.App.DataContext;
using TaskManagerServer.Lib.Core.TaskManagerServer;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerServer.Lib.App.Services;

public class TaskService(IDataContext dataContext, ICurrentUserService currentUserService) : ITaskService
{
    public async Task<List<GetTaskResponse>> GetAllAsync()
    {
        return await dataContext.TaskEntities.Select(x =>
        new GetTaskResponse
        {
            Id = x.Id,
            TaskTypeId = x.TaskTypeId,
            Status = x.Status,
            IsDeleted = x.Deleted.HasValue
        }).ToListAsync();
    }

    public async Task<GetTaskResponse> GetByIdAsync(int id)
    {
        return await dataContext.TaskEntities.Where(x => x.Id == id)
            .Select(x =>
            new GetTaskResponse
            {
                Id = x.Id,
                TaskTypeId = x.TaskTypeId,
                Status = x.Status,
                IsDeleted = x.Deleted.HasValue
            }).FirstAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entry = await dataContext.TaskEntities.SingleAsync(x => x.Id == id);
        entry.UpdatedBy = currentUserService.UserId;
        entry.Deleted = DateTime.UtcNow;
        await dataContext.SaveChangesAsync();
    }

    public async Task CreateAsync(CreateTaskRequest request)
    {
        var entity = new TaskEntity
        {
            TaskTypeId = request.TaskTypeId,
            CreatedBy = currentUserService.UserId,
            UpdatedBy = currentUserService.UserId,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow
        };
        dataContext.TaskEntities.Add(entity);
        await dataContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateTaskRequest request)
    {
        var entry = await dataContext.TaskEntities.SingleAsync(x => x.Id == id);
        entry.Status = request.Status;
        entry.Updated = DateTime.UtcNow;
        await dataContext.SaveChangesAsync();
    }
}