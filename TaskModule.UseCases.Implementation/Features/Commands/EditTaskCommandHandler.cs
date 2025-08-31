using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.UseCases.Interfaces.Commands;

namespace TaskModule.UseCases.Implementation.Features.Commands;

internal sealed class EditTaskCommandHandler(ITaskRepository taskRepository) : IRequestHandler<EditTaskCommand, Unit>
{
    public async Task<Unit> Handle(EditTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await taskRepository.CheckIfExists(request.TaskId))
            throw new NotFound("Task not found");

        if (!await taskRepository.CheckIfBelongsToUserAsync(request.TaskId, request.UserId))
            throw new Forbidden("Task doesn't belong to user");

        var task = await taskRepository.GetByIdAsync(request.TaskId);
        
        task.UpdateInfo(request.CreateTaskDto.Name, request.CreateTaskDto.Description, request.CreateTaskDto.EndDate, request.CreateTaskDto.SocialNodeId);
        
        await taskRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}