using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.Contracts.Commands;
using TaskModule.Contracts.Dtos.Requests;
using TaskModule.Contracts.Repositories;
using TaskModule.Domain.Entites;

namespace TaskModule.Application.Features.Commands;

public class EditTaskCommandHandler : IRequestHandler<EditTaskCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;

    public EditTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(EditTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await _taskRepository.CheckIfExists(request.TaskId))
            throw new NotFound("Task not found");

        if (!await _taskRepository.CheckIfBelongsToUserAsync(request.TaskId, request.UserId))
            throw new Forbidden("Task doesn't belong to user");

        var task = await _taskRepository.GetByIdAsync(request.TaskId);

        if (task != null) EditTask(request.CreateTaskDto, task);

        return Unit.Value;
    }

    private static void EditTask(CreateTaskDto request, TaskEntity task)
    {
        task.Name = request.Name;
        task.Description = request.Description;
        task.EndDate = request.EndDate;
        task.SocialNodeId = request.SocialNodeId;
    }
}