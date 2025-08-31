using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.Domain.Entities;
using EventModule.UseCases.Interfaces.Commands.EventType;
using MediatR;

namespace EventModule.UseCases.Implementation.Features.Commands.EventType;

internal sealed class CreateEvenTypeCommandHandler(IEventTypeRepository eventTypeRepository)
    : IRequestHandler<CreateEventTypeCommand, Unit>
{
    public async Task<Unit> Handle(CreateEventTypeCommand request, CancellationToken cancellationToken)
    {
        await eventTypeRepository.AddAsync(new EventTypeEntity(request.EventTypeRequestDto.Name,request.UserId));

        await eventTypeRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}