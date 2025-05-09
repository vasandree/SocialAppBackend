using Event.Contracts.Repositories;
using Event.Domain.Entities;
using Event.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;

namespace Event.Persistence.Repositories;

public class EventTypeRepository : BaseEntityRepository<EventTypeEntity>, IEventTypeRepository
{
    public EventTypeRepository(EventDbContext context) : base(context)
    {
    }

    public Task<IQueryable<EventTypeEntity>> GetByCreatorIdAsync(Guid creatorId)
    {
        return Task.FromResult(DbSet.Where(e => e.CreatorId == creatorId));
    }
}