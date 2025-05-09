using Event.Domain.Entities;
using Shared.Contracts.Repositories;

namespace Event.Contracts.Repositories;

public interface IEventTypeRepository : IBaseEntityRepository<EventTypeEntity>
{
    Task<IQueryable<EventTypeEntity>> GetByCreatorIdAsync(Guid creatorId);
}