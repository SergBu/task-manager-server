using Microsoft.AspNetCore.Mvc;
using TaskManagerServer.Lib.Core.Requests.V1;
using TaskManagerServer.Lib.Core.Responses.V1;
using TaskManagerServer.Lib.Core.TaskManagerServer;

namespace TaskManagerServer.App.Api.Controllers.V1;

[Route("/v{version:apiVersion}/task")]
[ApiVersion("1")]
[ApiController]
public class TaskController(ITaskService taskService) : ControllerBase()
{

    /// <summary>
    /// Метод для получения всех записей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<GetTaskResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
        var result = await taskService.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Метод для получения записи по id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetTaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await taskService.GetByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Метод для создания новой записи
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(CreateTaskRequest request)
    {
        await taskService.CreateAsync(request);
        return Created();
    }

    /// <summary>
    /// Метод для изменения планируемого параметра
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(int id, UpdateTaskRequest request)
    {
        await taskService.UpdateAsync(id, request);
        return NoContent();
    }

    /// <summary>
    /// Метод для удаления записи
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await taskService.DeleteAsync(id);
        return NoContent();
    }
}
