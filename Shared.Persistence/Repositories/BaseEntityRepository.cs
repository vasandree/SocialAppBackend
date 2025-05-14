using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Repositories;
using Shared.Domain;

namespace Shared.Persistence.Repositories;

public class BaseEntityRepository<T> : GenericRepository<T>, IBaseEntityRepository<T> where T : BaseEntity
{
    public BaseEntityRepository(DbContext context) : base(context)
    {
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<bool> CheckIfExists(Guid id)
    {
        return await DbSet.AnyAsync(x => x.Id == id);
    }
}