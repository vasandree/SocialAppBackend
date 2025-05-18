using Shared.Persistence.Repositories;
using Workspace.Contracts.Repositories;
using Workspace.Domain.Entities;
using Workspace.Infrastructure;

namespace Workspace.Persistence.Repositories;

public class WorkspaceUnitRepository : BaseEntityRepository<WorkspaceUnit>, IWorkspaceUnitRepository
{
    public WorkspaceUnitRepository(WorkspaceDbContext context) : base(context)
    {
    }
}