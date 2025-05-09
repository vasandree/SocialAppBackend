using Event.Contracts.Commands.EventType;
using Event.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;

namespace Event.Application.Features.Commands.EventType;

public class DeleteEventTypeCommandHandler : IRequestHandler<DeleteEventTypeCommand, Unit>
{
    private readonly IEventTypeRepository _eventTypeRepository;

    public DeleteEventTypeCommandHandler(IEventTypeRepository eventTypeRepository)
    {
        _eventTypeRepository = eventTypeRepository;
    }

    public async Task<Unit> Handle(DeleteEventTypeCommand request, CancellationToken cancellationToken)
    {
        if (!await _eventTypeRepository.CheckIfExists(request.EventTypeId))
            throw new NotFound("Provided event type does not exist");

        var eventType = await _eventTypeRepository.GetByIdAsync(request.EventTypeId);

        if (eventType.CreatorId != request.UserId) throw new Forbidden("You cannot delete this event type");

        await _eventTypeRepository.DeleteAsync(eventType);

        return Unit.Value;
    }
}