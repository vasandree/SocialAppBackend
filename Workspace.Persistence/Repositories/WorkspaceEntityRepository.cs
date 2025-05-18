using Shared.Persistence.Repositories;
using Workspace.Contracts.Repositories;
using Workspace.Domain.Entities;
using Workspace.Infrastructure;

namespace Workspace.Persistence.Repositories;

public class WorkspaceEntityRepository : BaseEntityRepository<WorkspaceEntity>, IWorkspaceEntityRepository
{
    public WorkspaceEntityRepository(WorkspaceDbContext context) : base(context)
    {
    }
}