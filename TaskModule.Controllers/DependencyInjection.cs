using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskModule.Application;
using TaskModule.Infrastructure;
using TaskModule.Persistence;

namespace TaskModule.Controllers;

public static class DependencyInjection
{
    public static void AddTaskModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddPersistence();
        services.AddApplication();
    }

    public static void UseTaskModule(this IServiceProvider services)
    {
        services.UseInfrastructure();
    }
}