using Shared.DataAccess.Interfaces;
using TaskModule.Domain.Entites;

namespace TaskModule.DataAccess.Interfaces.Repositories;

public interface ITaskRepository : IBaseEntityRepository<TaskEntity>
{
    Task<IReadOnlyList<TaskEntity>> GetAllByUserIdAsync(Guid userId);
    Task<bool> CheckIfBelongsToUserAsync(Guid userId, Guid taskId);
    Task DeleteByIdAsync(Guid id);
}