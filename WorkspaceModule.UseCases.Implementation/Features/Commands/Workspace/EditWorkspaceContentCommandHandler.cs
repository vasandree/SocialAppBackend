using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.UseCases.Interfaces.Commands.Workspace;
using WorkspaceModule.UseCases.Interfaces.Dtos.Responses;

namespace WorkspaceModule.UseCases.Implementation.Features.Commands.Workspace;

internal sealed class EditWorkspaceContentCommandHandler(IMapper mapper, IWorkspaceEntityRepository workspaceRepository)
    : IRequestHandler<EditWorkspaceContentCommand, WorkspaceResponseDto>
{
    public async Task<WorkspaceResponseDto> Handle(EditWorkspaceContentCommand request,
        CancellationToken cancellationToken)
    {
        if (!await workspaceRepository.CheckIfExists(request.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await workspaceRepository.GetByIdAsync(request.WorkspaceId);

        if (!workspace.IsUserCreator(request.UserId)) throw new Forbidden("You are not allowed to edit this workspace");
        
        workspace.UpdateContent(request.Content);
        
        await workspaceRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<WorkspaceResponseDto>(workspace);
    }
}