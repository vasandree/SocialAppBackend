using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using TaskModule.Contracts.Repositories;
using TaskModule.Domain.Entites;
using TaskModule.Infrastructure;

namespace TaskModule.Persistence.Repositories;

public class TaskRepository : BaseEntityRepository<TaskEntity>, ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TaskEntity>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Tasks.Where(x => x.SocialNodeId == userId).ToListAsync();
    }

    public async Task ChangeStatusAsync(Guid id,  Domain.Enums.StatusOfTask status)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        if (task != null) task.Status = status;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfBelongsToUserAsync(Guid userId, Guid taskId)
    {
        return await _context.Tasks.AnyAsync(x => x.Id == taskId && x.CreatorId == userId);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        if (task != null) _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}