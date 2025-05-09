using Event.Contracts.Repositories;
using Event.Domain.Entities;
using Event.Infrastructure;
using Shared.Persistence.Repositories;

namespace Event.Persistence.Repositories;

public class EventEntityRepository : BaseEntityRepository<EventEntity>, IEventEntityRepository
{
    public EventEntityRepository(EventDbContext context) : base(context)
    {
    }
}