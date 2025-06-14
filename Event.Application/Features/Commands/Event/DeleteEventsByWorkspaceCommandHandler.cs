using Event.Contracts.Commands.Event;
using Event.Contracts.Repositories;
using MediatR;

namespace Event.Application.Features.Commands.Event;

public class DeleteEventsByWorkspaceCommandHandler : IRequestHandler<DeleteEventsByWorkspaceCommand, Unit>
{
    private readonly IEventEntityRepository _eventRepository;

    public DeleteEventsByWorkspaceCommandHandler(IEventEntityRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(DeleteEventsByWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.FindAsync(x => x.WorkspaceId == request.WorkspaceId);

        await _eventRepository.RemoveRangeAsync(events);

        return Unit.Value;
    }
}