using System.Text.Json;
using EventModule.DataAccess.Interfaces.Repositories;
using NotificationModule.DataAccess.Interfaces;
using NotificationModule.Domain;
using NotificationModule.HttpClient.Interfaces;
using Quartz;
using TaskModule.DataAccess.Interfaces.Repositories;
using UserModule.DataAccess.Interfaces.Repositories;

namespace NotificationModule.BackgroundJobs;

internal sealed class OutboxNotificationJob(
    IOutboxMessageRepository outboxMessageRepository,
    INotificationSenderClient notificationClient,
    IUserRepository userRepository,
    ITaskRepository taskRepository,
    IEventEntityRepository eventRepository) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var nowUtc = DateTime.UtcNow;
        var cancellationToken = context.CancellationToken;

        var messages = await outboxMessageRepository.GetPendingAsync(nowUtc, 100, cancellationToken);
        if (messages.Count == 0) return;

        foreach (var message in messages)
        {
            var user = await userRepository.GetByIdAsync(message.UserId);
            var settings = user.UserSettings;

            if ((!settings.TaskNotifications && message.Type == OutboxMessageType.TaskReminder)
                || (!settings.EventNotifications && message.Type == OutboxMessageType.EventReminder))
            {
                message.Processed = true;
                message.ProcessedAtUtc = DateTime.UtcNow;
                continue;
            }

            var textToSend = message.Payload;

            switch (message.Type)
            {
                case OutboxMessageType.TaskReminder:
                {
                    var payloadObj = JsonSerializer.Deserialize<TaskReminderPayload>(message.Payload, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    var task = await taskRepository.GetByIdAsync(payloadObj.TaskId);
                    var language = settings.Language.ToString().ToLower();
                    textToSend = OutboxPayloadFactory.BuildTaskReminderText(
                        task.Name,
                        task.EndDate,
                        payloadObj.TimeZoneId,
                        language
                    ).Replace("{{UserName}}", user.UserName);
                    break;
                }
                case OutboxMessageType.EventReminder:
                {
                    var payloadObj = JsonSerializer.Deserialize<EventReminderPayload>(message.Payload, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    var eventEntity = await eventRepository.GetByIdAsync(payloadObj.EventId);
                    var language = settings.Language.ToString().ToLower();
                    textToSend = OutboxPayloadFactory.BuildEventReminderText(
                        eventEntity.Title,
                        eventEntity.Date,
                        eventEntity.Date,
                        payloadObj.TimeZoneId,
                        language
                    ).Replace("{{UserName}}", user.UserName);
                    break;
                }
            }

            var ok = await notificationClient.SendAsync(textToSend, settings, cancellationToken);
            if (!ok) continue;

            message.Processed = true;
            message.ProcessedAtUtc = DateTime.UtcNow;
        }

        await outboxMessageRepository.SaveChangesAsync(cancellationToken);
    }

    private sealed class TaskReminderPayload
    {
        public Guid TaskId { get; set; }
        public string TimeZoneId { get; set; } = string.Empty;
    }
    private sealed class EventReminderPayload
    {
        public Guid EventId { get; set; }
        public string TimeZoneId { get; set; } = string.Empty;
    }
}
