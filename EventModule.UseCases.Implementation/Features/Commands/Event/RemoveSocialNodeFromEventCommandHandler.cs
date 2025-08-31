using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.UseCases.Interfaces.Commands.Event;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.Domain.Entities;

namespace EventModule.UseCases.Implementation.Features.Commands.Event;

internal sealed class RemoveSocialNodeFromEventCommandHandler(
    IEventEntityRepository eventRepository,
    IBaseSocialNodeRepository<BaseSocialNode> nodeRepository)
    : IRequestHandler<RemoveSocialNodeFromEventCommand, Unit>
{
    public async Task<Unit> Handle(RemoveSocialNodeFromEventCommand request, CancellationToken cancellationToken)
    {
        if (!await eventRepository.CheckIfExists(request.EventId)) 
            throw new NotFound("Event not found");

        var eventEntity = await eventRepository.GetByIdAsync(request.EventId);

        if (!eventEntity.IsUserCreator( request.UserId)) 
            throw new Forbidden("You are not allowed to update this event");

        if (!await nodeRepository.CheckIfExists(request.NodeId))
            throw new NotFound("Node not found");

        eventEntity.RemoveSocialNode(request.NodeId);

        await eventRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}