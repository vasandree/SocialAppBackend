using Event.Contracts.Commands.Event;
using Event.Contracts.Repositories;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;

namespace Event.Application.Features.Commands.Event;

public class RemoveSocialNodeFromEventCommandHandler : IRequestHandler<RemoveSocialNodeFromEventCommand, Unit>
{
    private readonly IEventEntityRepository _eventRepository;
    private readonly IBaseSocialNodeRepository<BaseSocialNode> _nodeRepository;

    public RemoveSocialNodeFromEventCommandHandler(IEventEntityRepository eventRepository,
        IBaseSocialNodeRepository<BaseSocialNode> nodeRepository)
    {
        _eventRepository = eventRepository;
        _nodeRepository = nodeRepository;
    }

    public async Task<Unit> Handle(RemoveSocialNodeFromEventCommand request, CancellationToken cancellationToken)
    {
        if (!await _eventRepository.CheckIfExists(request.EventId)) throw new NotFound("Event not found");

        var eventEntity = await _eventRepository.GetByIdAsync(request.EventId);

        if (eventEntity.CreatorId != request.UserId) throw new Forbidden("You are not allowed to update this event");

        if (!await _nodeRepository.CheckIfExists(request.NodeId)) throw new NotFound("Node not found");

        eventEntity.SocialNodeId.Remove(request.NodeId);

        await _eventRepository.UpdateAsync(eventEntity);

        return Unit.Value;
    }
}