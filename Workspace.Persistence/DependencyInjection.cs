using Microsoft.Extensions.DependencyInjection;
using Workspace.Contracts.Repositories;
using Workspace.Persistence.Repositories;

namespace Workspace.Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IWorkspaceEntityRepository, WorkspaceEntityRepository>();
        services.AddTransient<IRelationRepository, RelationRepository>();
    }
}