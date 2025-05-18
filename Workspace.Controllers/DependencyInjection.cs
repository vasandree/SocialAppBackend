using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workspace.Infrastructure;

namespace Workspace.Controllers;

public static class DependencyInjection
{
    public static void AddWorkspaceModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        //services.AddPersistence();
        //services.AddApplication();
    }

    public static void UseWorkspaceModule(this IServiceProvider services)
    {
        services.UseInfrastructure();
    }
}