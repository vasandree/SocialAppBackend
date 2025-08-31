using EventModule.DataAccess.Interfaces.Repositories;
using EventModule.Domain.Entities;
using Shared.DataAccess.Implementation.Repositories;

namespace EventModule.DataAccess.Implementation.Repositories;

public class EventEntityRepository(EventDbContext context)
    : BaseEntityRepository<EventEntity>(context), IEventEntityRepository;