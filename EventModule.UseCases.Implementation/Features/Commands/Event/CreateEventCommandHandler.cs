using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.Domain.Entities;
using EventModule.UseCases.Interfaces.Commands.Event;
using MediatR;
using Shared.Domain.Exceptions;
using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.Domain.Entities;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using NotificationModule.Domain;
using NotificationModule.DataAccess.Implementation;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using UserModule.DataAccess.Interfaces.Repositories;

namespace EventModule.UseCases.Implementation.Features.Commands.Event;

internal sealed class CreateEventCommandHandler(
    IEventEntityRepository eventRepository,
    IBaseSocialNodeRepository<BaseSocialNode> nodeRepository,
    IEventTypeRepository eventTypeRepository,
    IWorkspaceEntityRepository workspaceRepository,
    NotificationDbContext notificationDbContext,
    IUserSettingsRepository userSettingsRepository)
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
        
        var userSettings = await userSettingsRepository.GetQueryableAsync().FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
        
        await eventRepository.AddAsync(eventEntity);

        var timeZoneId = userSettings?.TimeZoneId ?? TimeZoneInfo.Utc.Id;
        var scheduledAtUtc = OutboxUtils.GetScheduledUtcForReminder(request.CreateEventDto.Date, timeZoneId);
        
        var payloadObj = new {
            EventId = eventEntity.Id,
            TimeZoneId = timeZoneId
        };
        var payload = JsonSerializer.Serialize(payloadObj);

        var outbox = new OutboxMessage {
            Type = OutboxMessageType.EventReminder,
            Payload = payload,
            ScheduledAtUtc = scheduledAtUtc,
            Processed = false,
            UserId = request.UserId
        };
        await notificationDbContext.OutboxMessages.AddAsync(outbox, cancellationToken);
        await eventRepository.SaveChangesAsync(cancellationToken);
        await notificationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}