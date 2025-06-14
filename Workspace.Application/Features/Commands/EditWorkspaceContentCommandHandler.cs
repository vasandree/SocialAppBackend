using AutoMapper;
using MediatR;
using Shared.Domain.Exceptions;
using Workspace.Contracts.Commands;
using Workspace.Contracts.Dtos.Responses;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Commands;

public class EditWorkspaceContentCommandHandler : IRequestHandler<EditWorkspaceContentCommand, WorkspaceResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkspaceEntityRepository _workspaceRepository;

    public EditWorkspaceContentCommandHandler(IMapper mapper, IWorkspaceEntityRepository workspaceRepository)
    {
        _mapper = mapper;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<WorkspaceResponseDto> Handle(EditWorkspaceContentCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _workspaceRepository.CheckIfExists(request.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId);

        if (workspace.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit this workspace");
        
        workspace.ContentJson = request.Content;
        
        await _workspaceRepository.UpdateAsync(workspace);

        return _mapper.Map<WorkspaceResponseDto>(workspace);
    }
}