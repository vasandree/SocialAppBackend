using Microsoft.EntityFrameworkCore;
using NotificationModule.DataAccess.Interfaces;
using NotificationModule.Domain;
using Shared.DataAccess.Implementation.Repositories;

namespace NotificationModule.DataAccess.Implementation;

public class OutboxMessageRepository(NotificationDbContext context) : BaseEntityRepository<OutboxMessage>(context), IOutboxMessageRepository
{
    public async Task<List<OutboxMessage>> GetPendingAsync(DateTime nowUtc, int batchSize, CancellationToken cancellationToken)
        => await DbSet
            .Where(m => !m.Processed && m.ScheduledAtUtc <= nowUtc)
            .OrderBy(m => m.ScheduledAtUtc)
            .Take(batchSize)
            .ToListAsync(cancellationToken);
}