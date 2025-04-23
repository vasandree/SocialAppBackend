using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Repositories;
using Shared.Domain;

namespace Shared.Persistence.Repositories;

public class BaseEntityRepository<T> : GenericRepository<T>, IBaseEntityRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;

    public BaseEntityRepository(DbContext context) : base(context)
    {
        _dbSet = context.Set<T>();
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