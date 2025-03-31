using Common.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Common.Repositories.BaseEntityRepository;

public class BaseEntityRepository<T> : GenericRepository<T>, IBaseEntityRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _context;

    public BaseEntityRepository(DbContext context, DbSet<T> dbSet) : base(context)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> CheckIfExists(Guid id)
    {
        return await _dbSet.AnyAsync(x => x.Id == id);
    }
}