using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.UseCases.Interfaces.Commands.Event;
using MediatR;
using Shared.Domain.Exceptions;
using WorkspaceModule.DataAccess.Interfaces.Repositories;

namespace EventModule.UseCases.Implementation.Features.Commands.Event;

internal sealed class DeleteEventCommandHandler(
    IEventEntityRepository eventRepository,
    IWorkspaceEntityRepository workspaceRepository)
    : IRequestHandler<DeleteEventCommand, Unit>
{
    public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        if (!await eventRepository.CheckIfExists(request.EventId))
            throw new NotFound("Provided event does not exist");

        var eventEntity = await eventRepository.GetByIdAsync(request.EventId);

        if (!await workspaceRepository.CheckIfExists(eventEntity.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await workspaceRepository.GetByIdAsync(eventEntity.WorkspaceId);

        if (!eventEntity.IsUserCreator(request.UserId)) throw new BadRequest("You cannot delete this event");

        workspace.RemoveEvent(eventEntity.Id);

        eventRepository.DeleteAsync(eventEntity);
        
        await eventRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}