using Common.Repositories.BaseEntityRepository;
using Microsoft.EntityFrameworkCore;
using PersonService.Domain.Entities;

namespace PersonService.Persistence.Repositories.SocialNodeRepository;

public class SocialNodeRepository<T> : BaseEntityRepository<T>, ISocialNodeRepository<T> where T : BaseSocialNode
{
    private readonly PersosnsDbContext _context;

    public SocialNodeRepository(DbSet<T> dbSet, PersosnsDbContext context) : base(context, dbSet)
    {
        _context = context;
    }

    public Task<IQueryable<T>> GetAllAsQueryable(Guid userId)
    {
        return Task.FromResult(_context.Set<T>().Where(x => x.CreatorId == userId).AsNoTracking()
            .AsQueryable());
    }

    public async Task<bool> CheckIfUserIsCreator(Guid userId, Guid nodeId)
    {
        return await _context.Set<T>().AnyAsync(x => x.Id == nodeId && x.CreatorId == userId);
    }
}