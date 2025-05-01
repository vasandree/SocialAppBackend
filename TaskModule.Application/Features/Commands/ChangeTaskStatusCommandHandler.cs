using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.Contracts.Commands;
using TaskModule.Contracts.Repositories;

namespace TaskModule.Application.Features.Commands;

public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;

    public ChangeTaskStatusCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        if (!await _taskRepository.CheckIfExists(request.TaskId))
            throw new NotFound("Task not found");

        if (!await _taskRepository.CheckIfBelongsToUserAsync(request.TaskId, request.UserId))
            throw new Forbidden("Task doesn't belong to user");

        await _taskRepository.ChangeStatusAsync(request.TaskId, request.Status);
        
        return Unit.Value;
    }
}