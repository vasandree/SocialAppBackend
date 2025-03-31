using Common.Repositories.BaseEntityRepository;
using Microsoft.EntityFrameworkCore;
using PersonService.Domain;

namespace PersonService.Persistence.Repositories.SocialNodeRepository;

public class SocialNodeRepository : BaseEntityRepository<BaseSocialNode>, ISocialNodeRepository
{
    private readonly PersosnsDbContext _context;
    private readonly DbSet<BaseSocialNode> _dbSet;

    public SocialNodeRepository(DbSet<BaseSocialNode> dbSet, PersosnsDbContext context) : base(context, dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public async Task<IEnumerable<BaseSocialNode>> GetByName(string name, int page, int pageSize, Guid userId)
    {
        return await _dbSet.AsQueryable().Where(x => x.Name.Contains(name) && x.CreatorId == userId)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<BaseSocialNode>> GetAll(Guid userId, int page, int pageSize)
    {
        return await _dbSet.AsQueryable().Where(x => x.CreatorId == userId).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
    }
}