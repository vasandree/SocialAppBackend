using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using TaskModule.Contracts.Repositories;
using TaskModule.Domain.Entites;
using TaskModule.Infrastructure;

namespace TaskModule.Persistence.Repositories;

public class TaskRepository : BaseEntityRepository<TaskEntity>, ITaskRepository
{
    
    public TaskRepository(TaskDbContext context) : base(context)
    {
       
    }

    public async Task<IReadOnlyList<TaskEntity>> GetAllByUserIdAsync(Guid userId)
    {
        return await DbSet.Where(x => x.SocialNodeId == userId).ToListAsync();
    }

    public async Task ChangeStatusAsync(Guid id,  Domain.Enums.StatusOfTask status)
    {
        var task = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (task != null) task.Status = status;
        await Context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfBelongsToUserAsync(Guid userId, Guid taskId)
    {
        return await DbSet.AnyAsync(x => x.Id == taskId && x.CreatorId == userId);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var task = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (task != null) DbSet.Remove(task);
        await Context.SaveChangesAsync();
    }
}