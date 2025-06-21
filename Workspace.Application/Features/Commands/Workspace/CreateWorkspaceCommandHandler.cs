using MediatR;
using Workspace.Contracts.Commands;
using Workspace.Contracts.Repositories;
using Workspace.Domain.Entities;

namespace Workspace.Application.Features.Commands.Workspace;

public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, Guid>
{
    private readonly IWorkspaceEntityRepository _workspaceRepository;

    public CreateWorkspaceCommandHandler(IWorkspaceEntityRepository workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Guid> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var workspace = new WorkspaceEntity
        {
            Name = request.WorkspaceRequestDto.Name,
            Description = request.WorkspaceRequestDto.Description,
            CreatorId = request.UserId,
        };
        
        await _workspaceRepository.AddAsync(workspace);

        return workspace.Id;
    }
}