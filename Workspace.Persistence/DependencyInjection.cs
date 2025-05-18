using Microsoft.Extensions.DependencyInjection;
using Workspace.Contracts.Repositories;
using Workspace.Persistence.Repositories;

namespace Workspace.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IWorkspacePersonRepository, WorkspacePersonRepository>();
        services.AddTransient<IWorkspaceEntityRepository, WorkspaceEntityRepository>();
        services.AddTransient<IWorkspaceUnitRepository, WorkspaceUnitRepository>();
        services.AddTransient<IRelationRepository, RelationRepository>();
    }
}