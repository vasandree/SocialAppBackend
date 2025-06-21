using Event.Contracts.Commands.Event;
using Event.Contracts.Repositories;
using Event.Domain.Entities;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using Workspace.Contracts.Repositories;

namespace Event.Application.Features.Commands.Event;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Unit>
{
    private readonly IEventEntityRepository _eventRepository;
    private readonly IEventTypeRepository _eventTypeRepository;
    private readonly IBaseSocialNodeRepository<BaseSocialNode> _nodeRepository;
    private readonly IWorkspaceEntityRepository _workspaceRepository;

    public CreateEventCommandHandler(IEventEntityRepository eventRepository,
        IBaseSocialNodeRepository<BaseSocialNode> nodeRepository, IEventTypeRepository eventTypeRepository,
        IWorkspaceEntityRepository workspaceRepository)
    {
        _eventRepository = eventRepository;
        _nodeRepository = nodeRepository;
        _eventTypeRepository = eventTypeRepository;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (!await _workspaceRepository.CheckIfExists(request.CreateEventDto.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await _workspaceRepository.GetByIdAsync(request.CreateEventDto.WorkspaceId);
        if (workspace.CreatorId != request.UserId)
            throw new Forbidden("You are not allowed to create an event in this workspace");

        foreach (var nodeId in request.CreateEventDto.SocialNodeId)
        {
            var exists = await _nodeRepository.CheckIfExists(nodeId);
            if (!exists)
            {
                throw new BadRequest($"Social node with ID {nodeId} does not exist.");
            }
        }

        var eventEntity = new EventEntity();

        if (request.CreateEventDto.EventTypeId != null)
        {
            var eventTypeId = request.CreateEventDto.EventTypeId.Value;

            if (!await _eventTypeRepository.CheckIfExists(eventTypeId))
                throw new BadRequest("Provided event type does not exist");

            var eventType = await _eventTypeRepository.GetByIdAsync(eventTypeId);

            eventEntity.EventType = eventType;
            eventEntity.EventTypeId = eventType.Id;
        }

        eventEntity.SocialNodeId = request.CreateEventDto.SocialNodeId;
        eventEntity.WorkspaceId = request.CreateEventDto.WorkspaceId;
        eventEntity.Date = request.CreateEventDto.Date;
        eventEntity.Description = request.CreateEventDto.Description;
        eventEntity.Location = request.CreateEventDto.Location;
        eventEntity.Title = request.CreateEventDto.Title;

        await _eventRepository.AddAsync(eventEntity);

        return Unit.Value;
    }
}