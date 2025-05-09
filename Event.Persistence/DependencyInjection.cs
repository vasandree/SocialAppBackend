using Event.Contracts.Repositories;
using Event.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Event.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IEventEntityRepository, EventEntityRepository>();
        services.AddTransient<IEventTypeRepository, EventTypeRepository>();
    }
}