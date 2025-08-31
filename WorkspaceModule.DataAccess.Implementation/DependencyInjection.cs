using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkspaceModule.DataAccess.Implementation.Repositories;
using WorkspaceModule.DataAccess.Interfaces;
using WorkspaceModule.DataAccess.Interfaces.Repositories;

namespace WorkspaceModule.DataAccess.Implementation;

public static class DependencyInjection
{
    public static void AddWorkspaceModuleDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkspaceDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SocialAppDb")));
        
        services.AddTransient<IWorkspaceEntityRepository, WorkspaceEntityRepository>();
        services.AddTransient<IRelationRepository, RelationRepository>();
    }

    public static void UseWorkspaceModuleDataAccess(this IServiceProvider services)
    {
        using var serviceScope = services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<WorkspaceDbContext>();
        dbContext?.Database.Migrate();
    }
}