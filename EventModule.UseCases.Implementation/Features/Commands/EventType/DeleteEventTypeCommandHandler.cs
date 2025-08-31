using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.UseCases.Interfaces.Commands.EventType;
using MediatR;
using Shared.Domain.Exceptions;

namespace EventModule.UseCases.Implementation.Features.Commands.EventType;

internal sealed class DeleteEventTypeCommandHandler(IEventTypeRepository eventTypeRepository)
    : IRequestHandler<DeleteEventTypeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteEventTypeCommand request, CancellationToken cancellationToken)
    {
        if (!await eventTypeRepository.CheckIfExists(request.EventTypeId))
            throw new NotFound("Provided event type does not exist");

        var eventType = await eventTypeRepository.GetByIdAsync(request.EventTypeId);

        if (eventType.IsUserCreator(request.UserId)) throw new Forbidden("You cannot delete this event type");

        eventTypeRepository.DeleteAsync(eventType);

        await eventTypeRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}