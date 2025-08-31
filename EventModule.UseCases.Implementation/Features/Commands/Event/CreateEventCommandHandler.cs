using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.Domain.Entities;
using EventModule.UseCases.Interfaces.Commands.Event;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.Domain.Entities;
using WorkspaceModule.DataAccess.Interfaces.Repositories;

namespace EventModule.UseCases.Implementation.Features.Commands.Event;

internal sealed class CreateEventCommandHandler(
    IEventEntityRepository eventRepository,
    IBaseSocialNodeRepository<BaseSocialNode> nodeRepository,
    IEventTypeRepository eventTypeRepository,
    IWorkspaceEntityRepository workspaceRepository)
    : IRequestHandler<CreateEventCommand, Unit>
{
    public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (!await workspaceRepository.CheckIfExists(request.CreateEventDto.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await workspaceRepository.GetByIdAsync(request.CreateEventDto.WorkspaceId);
        
        if (!workspace.CheckIfUserIsCreator(request.UserId))
            throw new Forbidden("You are not allowed to create an event in this workspace");

        foreach (var nodeId in request.CreateEventDto.SocialNodeId)
        {
            if (!await nodeRepository.CheckIfExists(nodeId))
            {
                throw new BadRequest($"Social node with ID {nodeId} does not exist.");
            }
        }

        var eventEntity = new EventEntity(
            request.CreateEventDto.SocialNodeId,
            request.CreateEventDto.WorkspaceId, 
            request.CreateEventDto.Date,
            request.CreateEventDto.Description,
            request.CreateEventDto.Location,
            request.CreateEventDto.Title);

        if (request.CreateEventDto.EventTypeId != null)
        {
            var eventTypeId = request.CreateEventDto.EventTypeId.Value;

            if (!await eventTypeRepository.CheckIfExists(eventTypeId))
                throw new BadRequest("Provided event type does not exist");

            var eventType = await eventTypeRepository.GetByIdAsync(eventTypeId);

            eventEntity.AddEventType(eventType);
        }
        
        await eventRepository.AddAsync(eventEntity);

        await eventRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}