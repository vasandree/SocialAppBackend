using Shared.Contracts.Repositories;
using TaskModule.Domain.Entites;
using TaskModule.Domain.Enums;

namespace TaskModule.Contracts.Repositories;

public interface ITaskRepository : IBaseEntityRepository<TaskEntity>
{
    Task<IReadOnlyList<TaskEntity>> GetAllByUserIdAsync(Guid userId);
    Task ChangeStatusAsync(Guid id, StatusOfTask status);
    Task<bool> CheckIfBelongsToUserAsync(Guid userId, Guid taskId);
    Task DeleteByIdAsync(Guid id);
}