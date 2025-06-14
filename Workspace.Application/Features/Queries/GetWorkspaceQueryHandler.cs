using AutoMapper;
using Event.Contracts.Dtos.Responses;
using Event.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.Contracts.Dtos.Responses;
using TaskModule.Contracts.Repositories;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Queries;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Queries;

public class GetWorkspaceQueryHandler : IRequestHandler<GetWorkspaceQuery, WorkspaceResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkspaceEntityRepository _workspaceRepository;
    private readonly IEventEntityRepository _eventRepository;
    private readonly ITaskRepository _taskRepository;

    public GetWorkspaceQueryHandler(IMapper mapper, IWorkspaceEntityRepository workspaceRepository,
        ITaskRepository taskRepository, IEventEntityRepository eventRepository)
    {
        _mapper = mapper;
        _workspaceRepository = workspaceRepository;
        _taskRepository = taskRepository;
        _eventRepository = eventRepository;
    }

    public async Task<WorkspaceResponseDto> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
    {
        if (!await _workspaceRepository.CheckIfExists(request.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId);

        if (workspace.CreatorId != request.UserId) throw new Forbidden("You are not allowed to view this workspace");

        var tasks = await _taskRepository.FindAsync(x => x.WorkspaceId == workspace.Id);
        var events = await _eventRepository.FindAsync(x => x.WorkspaceId == workspace.Id);

        return new WorkspaceResponseDto
        {
            Info = _mapper.Map<WorkspaceInfoDto>(workspace),
            Tasks = _mapper.Map<TasksDto>(tasks),
            Events = _mapper.Map<IReadOnlyList<ListedEventDto>>(events)
        };
    }
}