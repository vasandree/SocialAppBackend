using Event.Contracts.Commands.Event;
using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.Contracts.Commands;
using Workspace.Contracts.Commands;
using Workspace.Contracts.Repositories;

namespace Workspace.Application.Features.Commands.Workspace;

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand, Unit>
{
    private readonly ISender _mediator;
    private readonly IWorkspaceEntityRepository _workspaceRepository;

    public DeleteWorkspaceCommandHandler(ISender mediator, IWorkspaceEntityRepository workspaceRepository)
    {
        _mediator = mediator;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Unit> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        if (!await _workspaceRepository.CheckIfExists(request.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId);

        if (workspace.CreatorId != request.UserId) throw new Forbidden("You are not allowed to delete this workspace");

        await _mediator.Send(new DeleteTasksByWorkspaceCommand(workspace.Id), cancellationToken);
        await _mediator.Send(new DeleteEventsByWorkspaceCommand(workspace.Id), cancellationToken);

        return Unit.Value;
    }
}