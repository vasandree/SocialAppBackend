using Shared.DataAccess.Interfaces;
using WorkspaceModule.Domain.Entities;

namespace WorkspaceModule.DataAccess.Interfaces.Repositories;

public interface IWorkspaceEntityRepository : IBaseEntityRepository<WorkspaceEntity>;