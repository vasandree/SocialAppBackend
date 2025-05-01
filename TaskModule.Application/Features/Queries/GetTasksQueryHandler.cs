using System.Runtime.Intrinsics.X86;
using AutoMapper;
using MediatR;
using TaskModule.Contracts.Dtos.Responses;
using TaskModule.Contracts.Queries;
using TaskModule.Contracts.Repositories;
using TaskModule.Domain.Enums;

namespace TaskModule.Application.Features.Queries;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, TasksDto>
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public GetTasksQueryHandler(IMapper mapper, ITaskRepository taskRepository)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    public async Task<TasksDto> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllByUserIdAsync(request.UserId);

        return new TasksDto
        {
            OpenTasks = _mapper.Map<IReadOnlyList<ListedTaskDto>>(tasks.Where(x => x.Status == StatusOfTask.Created)
                .ToList()),
            InProgressTasks =
                _mapper.Map<IReadOnlyList<ListedTaskDto>>(
                    tasks.Where(x => x.Status == StatusOfTask.InProgress).ToList()),
            CompletedTasks =
                _mapper.Map<IReadOnlyList<ListedTaskDto>>(tasks.Where(x => x.Status == StatusOfTask.Done).ToList()),
            CancelledTasks =
                _mapper.Map<IReadOnlyList<ListedTaskDto>>(tasks.Where(x => x.Status == StatusOfTask.Cancelled)
                    .ToList()),
        };
    }
}