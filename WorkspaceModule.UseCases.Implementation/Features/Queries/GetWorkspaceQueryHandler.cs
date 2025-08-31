using AutoMapper;
using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;
using WorkspaceModule.UseCases.Interfaces.Queries;

namespace WorkspaceModule.UseCases.Implementation.Features.Queries;

internal sealed class GetWorkspaceQueryHandler(
    IMapper mapper,
    IWorkspaceEntityRepository workspaceRepository,
    ITaskRepository taskRepository,
    IEventEntityRepository eventRepository)
    : IRequestHandler<GetWorkspaceQuery, WorkspaceResponseDto>
{
    public async Task<WorkspaceResponseDto> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
    {
        if (!await workspaceRepository.CheckIfExists(request.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await workspaceRepository.GetByIdAsync(request.WorkspaceId);

        if (!workspace.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to view this workspace");

        var tasks = await taskRepository.FindAsync(x => x.WorkspaceId == workspace.Id);
        var events = await eventRepository.FindAsync(x => x.WorkspaceId == workspace.Id);

        return new WorkspaceResponseDto(mapper.Map<WorkspaceInfoDto>(workspace), mapper.Map<TasksDto>(tasks),
            mapper.Map<IReadOnlyList<ListedEventDto>>(events));
    }
}