using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation.Repositories;
using TaskModule.DataAccess.Interfaces.Repositories;
using TaskModule.Domain.Entites;

namespace TaskModule.DataAccess.Implementation.Repositories;

public class TaskRepository(TaskDbContext context) : BaseEntityRepository<TaskEntity>(context), ITaskRepository
{
    public async Task<IReadOnlyList<TaskEntity>> GetAllByUserIdAsync(Guid userId)
        => await DbSet.Where(x => x.CreatorId == userId).ToListAsync();


    public async Task<bool> CheckIfBelongsToUserAsync(Guid userId, Guid taskId)
        => await DbSet.AnyAsync(x => x.Id == taskId && x.CreatorId == userId);


    public async Task DeleteByIdAsync(Guid id)
    {
        var task = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (task != null) DbSet.Remove(task);
    }
}