using Shared.Domain;

namespace NotificationModule.Domain;

public class OutboxMessage : BaseEntity
{
    public OutboxMessageType Type { get; init; }
    public required string Payload { get; init; }
    public DateTime ScheduledAtUtc { get; init; }
    public bool Processed { get; set; }
    public DateTime? ProcessedAtUtc { get; set; }
    public Guid UserId { get; set; }
}