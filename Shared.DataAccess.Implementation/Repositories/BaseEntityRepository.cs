using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Interfaces;
using Shared.Domain;

namespace Shared.DataAccess.Implementation.Repositories;

public class BaseEntityRepository<T>(CommonDbContext context) : GenericRepository<T>(context), IBaseEntityRepository<T>
    where T : BaseEntity
{
    public async Task<T> GetByIdAsync(Guid id)
        => await DbSet.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();


    public async Task<bool> CheckIfExists(Guid id) => await DbSet.AnyAsync(x => x.Id == id);
}