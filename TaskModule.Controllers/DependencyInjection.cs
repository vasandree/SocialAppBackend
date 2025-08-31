using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskModule.UseCases.Implementation;
using TaskModule.DataAccess.Implementation;

namespace TaskModule.Controllers;

public static class DependencyInjection
{
    public static void AddTaskModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTaskModuleDataAccess(configuration);
        services.AddTaskModuleUseCases();
    }

    public static void UseTaskModule(this IServiceProvider services)
    {
        services.UseTaskModuleDataAccess();
    }
}