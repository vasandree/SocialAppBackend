using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.Domain.Entities;
using EventModule.UseCases.Interfaces.Commands.Event;
using EventModule.UseCases.Interfaces.Dtos.Requests;
using MediatR;
using Shared.Domain.Exceptions;

namespace EventModule.UseCases.Implementation.Features.Commands.Event;

internal sealed class UpdateEventCommandHandler(
    IEventTypeRepository eventTypeRepository,
    IEventEntityRepository eventRepository)
    : IRequestHandler<UpdateEventCommand, Unit>
{
    public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        if (!await eventRepository.CheckIfExists(request.EventId))
            throw new NotFound("Provided event does not exist");

        var eventEntity = await eventRepository.GetByIdAsync(request.EventId);

        if (!eventEntity.IsUserCreator(request.UserId)) throw new Forbidden("You cannot edit this event");

        await UpdateEvent(eventEntity, request.UpdateEventDto);

        await eventRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task UpdateEvent(EventEntity eventEntity, UpdateEventDto updateEventDto)
    {
        eventEntity.UpdateInfo(updateEventDto.Date, updateEventDto.Description, updateEventDto.Location,updateEventDto.Title);

        if (eventEntity.CheckIfEventType(updateEventDto.EventTypeId))
        {
            if (updateEventDto.EventTypeId != null)
            {
                if (!await eventTypeRepository.CheckIfExists(updateEventDto.EventTypeId.Value))
                {
                    throw new BadRequest("Provided event type does not exist.");
                }

                eventEntity.AddEventType(await eventTypeRepository.GetByIdAsync(updateEventDto.EventTypeId.Value));
            }
            else
            {
                eventEntity.RemoveEventType();
            }
        }
    }
}