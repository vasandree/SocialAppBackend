using EventModule.Domain.Entities;
using Shared.DataAccess.Interfaces;

namespace EventModule.DataAccess.Interfaces.Repositories;

public interface IEventTypeRepository : IBaseEntityRepository<EventTypeEntity>
{
    IQueryable<EventTypeEntity> GetByCreatorIdAsync(Guid creatorId);
}