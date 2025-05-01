using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.Contracts.Dtos.Responses;
using TaskModule.Contracts.Queries;
using TaskModule.Contracts.Repositories;

namespace TaskModule.Application.Features.Queries;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQueryHandler(IMapper mapper, ITaskRepository taskRepository)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        if (!await _taskRepository.CheckIfExists(request.TaskId))
            throw new NotFound("Task not found");

        if (!await _taskRepository.CheckIfBelongsToUserAsync(request.TaskId, request.UserId))
            throw new Forbidden("Task doesn't belong to user");

        return _mapper.Map<TaskDto>(await _taskRepository.GetByIdAsync(request.TaskId));
    }
}