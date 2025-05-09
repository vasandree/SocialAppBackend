using Event.Contracts.Commands.EventType;
using Event.Contracts.Repositories;
using Event.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;

namespace Event.Application.Features.Commands.EventType;

public class EditEventTypeCommandHandler : IRequestHandler<EditEventTypeCommand, Unit>
{
    private readonly IEventTypeRepository _eventTypeRepository;

    public EditEventTypeCommandHandler(IEventTypeRepository eventTypeRepository)
    {
        _eventTypeRepository = eventTypeRepository;
    }

    public async Task<Unit> Handle(EditEventTypeCommand request, CancellationToken cancellationToken)
    {
        if (!await _eventTypeRepository.CheckIfExists(request.EventTypeId))
            throw new NotFound("Provided event type does not exist");

        var eventType = await _eventTypeRepository.GetByIdAsync(request.EventTypeId);

        if (eventType.CreatorId != request.UserId) throw new Forbidden("You cannot edit this event type");

        eventType.Name = request.EventTypeRequestDto.Name;

        await _eventTypeRepository.UpdateAsync(eventType);

        return Unit.Value;
    }
}