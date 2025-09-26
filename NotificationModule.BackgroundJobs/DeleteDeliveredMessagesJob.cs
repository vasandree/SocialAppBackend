using NotificationModule.DataAccess.Interfaces;
using Quartz;

namespace NotificationModule.BackgroundJobs;

internal sealed class DeleteDeliveredMessagesJob(IOutboxMessageRepository outboxMessageRepository) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var cancellationToken = context.CancellationToken;

        var messages = await outboxMessageRepository.GetDeliveredMessages(cancellationToken);
        if (messages.Count == 0) return;
        
        outboxMessageRepository.RemoveRange(messages);
        await outboxMessageRepository.SaveChangesAsync(cancellationToken);
        
        Console.WriteLine($"Deleted {messages.Count} delivered outbox messages.");
    }
}