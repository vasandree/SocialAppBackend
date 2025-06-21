using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using Workspace.Contracts.Commands;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Commands.Workspace;

public class EditWorkspaceCommandHandler : IRequestHandler<EditWorkspaceCommand, ListedWorkspaceDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkspaceEntityRepository _workspaceRepository;

    public EditWorkspaceCommandHandler(IWorkspaceEntityRepository workspaceRepository, IMapper mapper)
    {
        _workspaceRepository = workspaceRepository;
        _mapper = mapper;
    }

    public async Task<ListedWorkspaceDto> Handle(EditWorkspaceCommand request, CancellationToken cancellationToken)
    {
        if (!await _workspaceRepository.CheckIfExists(request.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId);

        if (workspace.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit this workspace");

        workspace.Name = request.WorkspaceRequestDto.Name;
        workspace.Description = request.WorkspaceRequestDto.Description;

        await _workspaceRepository.UpdateAsync(workspace);

        return _mapper.Map<ListedWorkspaceDto>(workspace);
    }
}