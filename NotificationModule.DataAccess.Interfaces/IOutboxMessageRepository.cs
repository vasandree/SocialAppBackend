using NotificationModule.Domain;
using Shared.DataAccess.Interfaces;

namespace NotificationModule.DataAccess.Interfaces;

public interface IOutboxMessageRepository : IBaseEntityRepository<OutboxMessage>
{
    Task<List<OutboxMessage>> GetPendingAsync(DateTime nowUtc, int batchSize, CancellationToken cancellationToken);
}