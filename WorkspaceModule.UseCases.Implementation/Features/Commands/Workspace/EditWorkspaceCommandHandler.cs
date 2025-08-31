using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Commands.Workspace;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Implementation.Features.Commands.Workspace;

internal sealed class EditWorkspaceCommandHandler(IWorkspaceEntityRepository workspaceRepository, IMapper mapper)
    : IRequestHandler<EditWorkspaceCommand, ListedWorkspaceDto>
{
    public async Task<ListedWorkspaceDto> Handle(EditWorkspaceCommand request, CancellationToken cancellationToken)
    {
        if (!await workspaceRepository.CheckIfExists(request.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await workspaceRepository.GetByIdAsync(request.WorkspaceId);

        if (!workspace.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to edit this workspace");

        workspace.UpdateInfo(request.WorkspaceRequestDto.Name,request.WorkspaceRequestDto.Description);

        await workspaceRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<ListedWorkspaceDto>(workspace);
    }
}