using EventModule.Domain.Entities;
using Shared.DataAccess.Interfaces;

namespace EventModule.DataAccess.Interfaces.Repositories;

public interface IEventEntityRepository : IBaseEntityRepository<EventEntity>;