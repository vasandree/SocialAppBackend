using Microsoft.AspNetCore.Builder;
using TaskModule.Application;
using TaskModule.Infrastructure;
using TaskModule.Persistence;

namespace TaskModule.Controllers;

public static class DependencyInjection
{
    public static void AddTaskModule(this WebApplicationBuilder builder)
    {
        builder.AddInfrastructure();
        builder.AddPersistence();
        builder.AddApplication();
    }

    public static void UseTaskModule(this WebApplication app)
    {
        app.UseInfrastructure();
    }
}