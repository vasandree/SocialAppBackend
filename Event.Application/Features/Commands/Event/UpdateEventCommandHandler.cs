using Event.Contracts.Commands.Event;
using Event.Contracts.Dtos.Requests;
using Event.Contracts.Repositories;
using Event.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;

namespace Event.Application.Features.Commands.Event;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Unit>
{
    private readonly IEventTypeRepository _eventTypeRepository;
    private readonly IEventEntityRepository _eventRepository;

    public UpdateEventCommandHandler(IEventTypeRepository eventTypeRepository, IEventEntityRepository eventRepository)
    {
        _eventTypeRepository = eventTypeRepository;
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        if (!await _eventRepository.CheckIfExists(request.EventId))
            throw new NotFound("Provided event does not exist");

        var eventEntity = await _eventRepository.GetByIdAsync(request.EventId);

        if (eventEntity.CreatorId != request.UserId) throw new Forbidden("You cannot edit this event");

        await UpdateEvent(eventEntity, request.UpdateEventDto);

        await _eventRepository.UpdateAsync(eventEntity);

        return Unit.Value;
    }

    private async Task UpdateEvent(EventEntity eventEntity, UpdateEventDto updateEventDto)
    {
        eventEntity.Date = updateEventDto.Date;
        eventEntity.Description = updateEventDto.Description;
        eventEntity.Location = updateEventDto.Location;
        eventEntity.Title = updateEventDto.Title;

        if (eventEntity.EventTypeId != updateEventDto.EventTypeId)
        {
            if (updateEventDto.EventTypeId != null)
            {
                if (!await _eventTypeRepository.CheckIfExists(updateEventDto.EventTypeId.Value))
                {
                    throw new BadRequest("Provided event type does not exist.");
                }

                eventEntity.EventTypeId = updateEventDto.EventTypeId;
                eventEntity.EventType = await _eventTypeRepository.GetByIdAsync(updateEventDto.EventTypeId.Value);
            }
            else
            {
                eventEntity.EventTypeId = null;
                eventEntity.EventType = null;
            }
        }
    }
}