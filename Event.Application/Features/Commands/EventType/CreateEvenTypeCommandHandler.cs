using Event.Contracts.Commands.EventType;
using Event.Contracts.Repositories;
using Event.Domain.Entities;
using MediatR;

namespace Event.Application.Features.Commands.EventType;

public class CreateEvenTypeCommandHandler : IRequestHandler<CreateEventTypeCommand, Unit>
{
    private readonly IEventTypeRepository _eventTypeRepository;

    public CreateEvenTypeCommandHandler(IEventTypeRepository eventTypeRepository)
    {
        _eventTypeRepository = eventTypeRepository;
    }

    public async Task<Unit> Handle(CreateEventTypeCommand request, CancellationToken cancellationToken)
    {
        await _eventTypeRepository.AddAsync(new EventTypeEntity
        {
            Name = request.EventTypeRequestDto.Name,
            CreatorId = request.UserId
        });

        return Unit.Value;
    }
}