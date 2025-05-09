using Event.Domain.Entities;
using Shared.Contracts.Repositories;

namespace Event.Contracts.Repositories;

public interface IEventEntityRepository : IBaseEntityRepository<EventEntity>
{
}