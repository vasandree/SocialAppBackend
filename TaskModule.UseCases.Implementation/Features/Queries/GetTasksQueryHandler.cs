using AutoMapper;
using MediatR;
using SocialNodeModule.UseCases.Interfaces.Dtos.Responses;
using SocialNodeModule.UseCases.Interfaces.Services;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.UseCases.Interfaces.Dtos.Responses;
using TaskModule.UseCases.Interfaces.Queries;
using TaskModule.Domain.Enums;

namespace TaskModule.UseCases.Implementation.Features.Queries;

internal sealed class GetTasksQueryHandler(
    IMapper mapper,
    ITaskRepository taskRepository,
    ISocialNodeService socialNodeService)
    : IRequestHandler<GetTasksQuery, TasksDto>
{
    public async Task<TasksDto> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.GetAllByUserIdAsync(request.UserId);

        var socialNodesList = await socialNodeService.GetAllNodesForUser(request.UserId, cancellationToken);

        var socialNodeDict = socialNodesList.ToDictionary(x => x.Id);

        var cancelledTasksDto = new List<ListedTaskDto>();
        var openTasksDto = new List<ListedTaskDto>();
        var completedTasksDto = new List<ListedTaskDto>();
        var inProgressTasksDto = new List<ListedTaskDto>();

        foreach (var task in tasks)
        {
            var taskDto = mapper.Map<ListedTaskDto>(task);

            if (socialNodeDict.TryGetValue(task.SocialNodeId, out var socialNode))
            {
                taskDto.SocialNode = mapper.Map<ListedBaseSocialNodeDto>(socialNode);
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return new TasksDto(openTasksDto, inProgressTasksDto, completedTasksDto, cancelledTasksDto);
    }
}