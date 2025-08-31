using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.UseCases.Interfaces.Commands.Event;
using MediatR;

namespace EventModule.UseCases.Implementation.Features.Commands.Event;

internal sealed class DeleteEventsByWorkspaceCommandHandler(IEventEntityRepository eventRepository)
    : IRequestHandler<DeleteEventsByWorkspaceCommand, Unit>
{
    public async Task<Unit> Handle(DeleteEventsByWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var events = await eventRepository.FindAsync(x => x.WorkspaceId == request.WorkspaceId);

        eventRepository.RemoveRangeAsync(events);
        
        await eventRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}