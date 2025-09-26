using System.Text.Json;
using MediatR;
using Shared.Domain.Exceptions;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.UseCases.Interfaces.Commands;
using TaskModule.Domain.Entites;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using NotificationModule.Domain;
using NotificationModule.DataAccess.Implementation;
using Microsoft.EntityFrameworkCore;
using UserModule.DataAccess.Interfaces.Repositories;

namespace TaskModule.UseCases.Implementation.Features.Commands;

internal sealed class CreateTaskCommandHandler(
    ITaskRepository repository,
    IWorkspaceEntityRepository workspaceRepository,
    NotificationDbContext notificationDbContext,
    IUserSettingsRepository userSettingsRepository)
    : IRequestHandler<CreateTaskCommand, Unit>
{
    public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        if (!await workspaceRepository.CheckIfExists(request.CreateTaskDto.WorkspaceId))
            throw new NotFound("Workspace not found");

        var workspace = await workspaceRepository.GetByIdAsync(request.CreateTaskDto.WorkspaceId);

        var task = new TaskEntity(
            request.CreateTaskDto.Name,
            request.CreateTaskDto.Description,
            request.CreateTaskDto.StartDate,
            request.CreateTaskDto.EndDate,
            request.CreateTaskDto.SocialNodeId,
            request.UserId,
            workspace.Id
        );

        workspace.Tasks.Add(task.Id);
        await repository.AddAsync(task);

        var userSettings = await userSettingsRepository.GetQueryableAsync()
            .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        var timeZoneId = userSettings?.TimeZoneId ?? TimeZoneInfo.Utc.Id;
        var scheduledAtUtc = OutboxUtils.GetScheduledUtcForReminder(request.CreateTaskDto.StartDate, timeZoneId);

        var payloadObj = new {
            TaskId = task.Id,
            TimeZoneId = timeZoneId
        };
        var payload = JsonSerializer.Serialize(payloadObj);

        var outbox = new OutboxMessage
        {
            Type = OutboxMessageType.TaskReminder,
            Payload = payload,
            ScheduledAtUtc = scheduledAtUtc,
            Processed = false,
            UserId = request.UserId
        };

        await notificationDbContext.OutboxMessages.AddAsync(outbox, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);
        await notificationDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
