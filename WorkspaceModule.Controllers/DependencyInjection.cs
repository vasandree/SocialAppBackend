using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkspaceModule.UseCases.Implementation;
using WorkspaceModule.DataAccess.Implementation;

namespace WorkspaceModule.Controllers;

public static class DependencyInjection
{
    public static void AddWorkspaceModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddWorkspaceModuleDataAccess(configuration); 
        services.AddApplication();
    }

    public static void UseWorkspaceModule(this IServiceProvider services)
    {
        services.UseWorkspaceModuleDataAccess();
    }
}