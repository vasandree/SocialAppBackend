using Shared.DataAccess.Implementation.Repositories;
using WorkspaceModule.DataAccess.Interfaces.Repositories;
using WorkspaceModule.Domain.Entities;

namespace WorkspaceModule.DataAccess.Implementation.Repositories;

public class WorkspaceEntityRepository(WorkspaceDbContext context)
    : BaseEntityRepository<WorkspaceEntity>(context), IWorkspaceEntityRepository;