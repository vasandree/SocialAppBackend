using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskModule.Contracts.Repositories;
using TaskModule.Persistence.Repositories;

namespace TaskModule.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ITaskRepository, TaskRepository>();
    }
}