using Event.Contracts.Commands.Event;
using Event.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;

namespace Event.Application.Features.Commands.Event;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
{
    private readonly IEventEntityRepository _eventRepository;

    public DeleteEventCommandHandler(IEventEntityRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        if (!await _eventRepository.CheckIfExists(request.EventId))
            throw new NotFound("Provided event does not exist");

        var eventEntity = await _eventRepository.GetByIdAsync(request.EventId);

        if (eventEntity.CreatorId != request.UserId) throw new Exception("You cannot delete this event");

        await _eventRepository.DeleteAsync(eventEntity);

        return Unit.Value;
    }
}