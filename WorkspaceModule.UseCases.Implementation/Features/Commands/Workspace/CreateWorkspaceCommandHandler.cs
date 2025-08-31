using MediatR;
using WorkspaceModule.UseCases.Interfaces.Commands.Workspace;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.Domain.Entities;

namespace WorkspaceModule.UseCases.Implementation.Features.Commands.Workspace;

internal sealed class CreateWorkspaceCommandHandler(IWorkspaceEntityRepository workspaceRepository)
    : IRequestHandler<CreateWorkspaceCommand, Guid>
{
    public async Task<Guid> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = new WorkspaceEntity(request.WorkspaceRequestDto.Name, request.WorkspaceRequestDto.Description,
            request.UserId);
        
        await workspaceRepository.AddAsync(workspace);

        await workspaceRepository.SaveChangesAsync(cancellationToken);
        
        return workspace.Id;
    }
}