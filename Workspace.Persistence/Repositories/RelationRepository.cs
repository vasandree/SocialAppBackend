using Shared.Persistence.Repositories;
using Workspace.Contracts.Repositories;
using Workspace.Domain.Entities;
using Workspace.Infrastructure;

namespace Workspace.Persistence.Repositories;

public class RelationRepository : BaseEntityRepository<RelationEntity>, IRelationRepository
{
    public RelationRepository(WorkspaceDbContext context) : base(context)
    {
    }
}