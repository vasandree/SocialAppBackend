using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Extensions;
using TaskModule.Contracts.Commands;
using TaskModule.Contracts.Dtos.Requests;
using TaskModule.Contracts.Queries;
using TaskModule.Domain.Enums;

namespace TaskModule.Controllers.Controllers;

[Authorize(Policy = "UserExists")]
[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly ISender _mediator;

    public TasksController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        return Ok(await _mediator.Send(new CreateTaskCommand(User.GetUserId()!.Value, createTaskDto)));
    }

    [HttpPut]
    [Route("{taskId}")]
    public async Task<IActionResult> EditTask(Guid taskId, CreateTaskDto createTaskDto)
    {
        return Ok(await _mediator.Send(new EditTaskCommand(User.GetUserId()!.Value, taskId, createTaskDto)));
    }

    [HttpDelete]
    [Route("{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        return Ok(await _mediator.Send(new DeleteTaskCommand(User.GetUserId()!.Value, taskId)));
    }

    [HttpPut]
    [Route("{taskId}/status")]
    public async Task<IActionResult> ChangeStatus(Guid taskId, [FromBody] StatusOfTask status)
    {
        return Ok(await _mediator.Send(new ChangeTaskStatusCommand(User.GetUserId()!.Value, taskId, status)));
    }

    [HttpGet]
    [Route("{taskId}")]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        return Ok(await _mediator.Send(new GetTaskByIdQuery(User.GetUserId()!.Value, taskId)));
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        return Ok(await _mediator.Send(new GetTasksQuery(User.GetUserId()!.Value)));
    }
}