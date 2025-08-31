using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.UseCases.Interfaces.Dtos.Responses;
using TaskModule.UseCases.Interfaces.Queries;

namespace TaskModule.UseCases.Implementation.Features.Queries;

internal sealed class GetTaskByIdQueryHandler(IMapper mapper, ITaskRepository taskRepository)
    : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
    public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        if (!await taskRepository.CheckIfExists(request.TaskId))
            throw new NotFound("Task not found");

        if (!await taskRepository.CheckIfBelongsToUserAsync(request.TaskId, request.UserId))
            throw new Forbidden("Task doesn't belong to user");

        return mapper.Map<TaskDto>(await taskRepository.GetByIdAsync(request.TaskId));
    }
}