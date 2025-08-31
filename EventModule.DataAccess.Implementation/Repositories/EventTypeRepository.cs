using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.Domain.Entities;
using Shared.DataAccess.Implementation.Repositories;

namespace EventModule.DataAccess.Implementation.Repositories;

public class EventTypeRepository(EventDbContext context)
    : BaseEntityRepository<EventTypeEntity>(context), IEventTypeRepository
{
    public IQueryable<EventTypeEntity> GetByCreatorIdAsync(Guid creatorId)
        => DbSet.Where(e => e.CreatorId == creatorId);
}