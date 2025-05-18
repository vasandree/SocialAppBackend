using Shared.Persistence.Repositories;
using Workspace.Contracts.Repositories;
using Workspace.Domain.Entities;
using Workspace.Infrastructure;

namespace Workspace.Persistence.Repositories;

public class WorkspacePersonRepository : BaseEntityRepository<WorkspacePerson>, IWorkspacePersonRepository
{
    public WorkspacePersonRepository(WorkspaceDbContext context) : base(context)
    {
    }
}