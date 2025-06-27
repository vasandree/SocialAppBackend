using AutoMapper;
using MediatR;
using SocialNode.Contracts.Dtos.Responses;
using SocialNode.Contracts.Services;
using TaskModule.Contracts.Dtos.Responses;
using TaskModule.Contracts.Queries;
using TaskModule.Contracts.Repositories;
using TaskModule.Domain.Enums;

namespace TaskModule.Application.Features.Queries;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, TasksDto>
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;
    private readonly ISocialNodeService _socialNodeService;

    public GetTasksQueryHandler(IMapper mapper, ITaskRepository taskRepository, ISocialNodeService socialNodeService)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
        _socialNodeService = socialNodeService;
    }

    public async Task<TasksDto> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllByUserIdAsync(request.UserId);

        var socialNodesList = await _socialNodeService.GetAllNodesForUser(request.UserId, cancellationToken);
        
        var socialNodeDict = socialNodesList.ToDictionary(x => x.Id);

        var cancelledTasksDto = new List<ListedTaskDto>();
        var openTasksDto = new List<ListedTaskDto>();
        var completedTasksDto = new List<ListedTaskDto>();
        var inProgressTasksDto = new List<ListedTaskDto>();

        foreach (var task in tasks)
        {
            var taskDto = _mapper.Map<ListedTaskDto>(task);

            if (socialNodeDict.TryGetValue(task.SocialNodeId, out var socialNode))
            {
                taskDto.SocialNode = _mapper.Map<ListedBaseSocialNodeDto>(socialNode);
            }

            switch (task.Status)
            {
                case StatusOfTask.Cancelled:
                    cancelledTasksDto.Add(taskDto);
                    break;
                case StatusOfTask.Created:
                    openTasksDto.Add(taskDto);
                    break;
                case StatusOfTask.Done:
                    completedTasksDto.Add(taskDto);
                    break;
                case StatusOfTask.InProgress:
                    inProgressTasksDto.Add(taskDto);
                    break;
            }
        }

        return new TasksDto
        {
            OpenTasks = openTasksDto,
            InProgressTasks = inProgressTasksDto,
            CompletedTasks = completedTasksDto,
            CancelledTasks = cancelledTasksDto
        };
    }
}