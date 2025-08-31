
using EventModule.DataAccess.Implementation;
using EventModule.UseCases.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventModule.Controllers;

public static class DependencyInjection
{
    public static void AddEventModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventModuleUseCases();
        services.AddEventModuleDataAccess(configuration);
    }

    public static void UseEventModule(this IServiceProvider services)
    {
        services.UseEventModuleDataAccess();
    }
}