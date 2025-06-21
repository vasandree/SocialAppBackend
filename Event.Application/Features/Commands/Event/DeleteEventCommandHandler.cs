using Event.Contracts.Commands.Event;
using Event.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;
using Workspace.Contracts.Repositories;

namespace Event.Application.Features.Commands.Event;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
{
    private readonly IEventEntityRepository _eventRepository;
    private readonly IWorkspaceEntityRepository _workspaceRepository;

    public DeleteEventCommandHandler(IEventEntityRepository eventRepository,
        IWorkspaceEntityRepository workspaceRepository)
    {
        _eventRepository = eventRepository;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        if (!await _eventRepository.CheckIfExists(request.EventId))
            throw new NotFound("Provided event does not exist");

        var eventEntity = await _eventRepository.GetByIdAsync(request.EventId);

        if (!await _workspaceRepository.CheckIfExists(eventEntity.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await _workspaceRepository.GetByIdAsync(eventEntity.WorkspaceId);

        if (eventEntity.CreatorId != request.UserId) throw new Exception("You cannot delete this event");

        workspace.Events.Remove(eventEntity.Id);

        await _eventRepository.DeleteAsync(eventEntity);
        await _workspaceRepository.UpdateAsync(workspace);

        return Unit.Value;
    }
}