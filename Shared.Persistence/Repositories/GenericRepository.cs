using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Repositories;

namespace Shared.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DbSet<T> DbSet;
    protected readonly DbContext Context;

    public GenericRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>?> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
        return Context.SaveChangesAsync();
    }

    public Task<IQueryable<T>> GetQueryableAsync()
    {
        return Task.FromResult(DbSet.AsNoTracking().AsQueryable());
    }
}