using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using TaskModule.Domain.Enums;
using TaskModule.UseCases.Interfaces.Commands;
using TaskModule.UseCases.Interfaces.Dtos.Requests;
using TaskModule.UseCases.Interfaces.Dtos.Responses;
using TaskModule.UseCases.Interfaces.Queries;

namespace TaskModule.Controllers.Controllers;

[Authorize]
[UserExists]
[ApiController]
[Route("tasks")]
public sealed class TasksController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        => Ok(await mediator.Send(new CreateTaskCommand(User.GetUserId(), createTaskDto)));
    

    [HttpPut]
    [Route("{taskId}")]
    public async Task<IActionResult> EditTask(Guid taskId, CreateTaskDto createTaskDto)
        => Ok(await mediator.Send(new EditTaskCommand(User.GetUserId(), taskId, createTaskDto)));
    

    [HttpDelete]
    [Route("{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
        => Ok(await mediator.Send(new DeleteTaskCommand(User.GetUserId(), taskId)));
    

    [HttpPut]
    [Route("{taskId}/status")]
    public async Task<IActionResult> ChangeStatus(Guid taskId, [FromBody] StatusOfTask status)
        => Ok(await mediator.Send(new ChangeTaskStatusCommand(User.GetUserId(), taskId, status)));
    

    [HttpGet]
    [Route("{taskId}")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTask(Guid taskId)
        => Ok(await mediator.Send(new GetTaskByIdQuery(User.GetUserId(), taskId)));
    

    [HttpGet]
    [ProducesResponseType(typeof(TasksDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTasks()
        => Ok(await mediator.Send(new GetTasksQuery(User.GetUserId())));
}