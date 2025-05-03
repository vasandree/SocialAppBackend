using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application;
using User.Infrastructure;
using User.Persistence;

namespace User.Controllers;

public static class DependencyInjection
{
    public static void AddUserModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddPersistence();
        services.AddInfrastructure(configuration);
    }

    public static void UseUserModule(this IServiceProvider services)
    {
        services.UseInfrastructure();
    }
}