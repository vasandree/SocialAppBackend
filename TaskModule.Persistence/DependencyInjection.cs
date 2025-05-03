using Microsoft.Extensions.DependencyInjection;
using TaskModule.Contracts.Repositories;
using TaskModule.Persistence.Repositories;

namespace TaskModule.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<ITaskRepository, TaskRepository>();
    }
}