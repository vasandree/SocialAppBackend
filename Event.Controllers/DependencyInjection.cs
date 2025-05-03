using Event.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Event.Controllers;

public static class DependencyInjection
{
    public static void AddEventModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
    }

    public static void UseEventModule(this IServiceProvider services)
    {
        services.UseInfrastructure();
    }
}