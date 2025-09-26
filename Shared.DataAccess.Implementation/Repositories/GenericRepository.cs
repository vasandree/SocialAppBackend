using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.DataAccess.Interfaces;

namespace Shared.DataAccess.Implementation.Repositories;

public class GenericRepository<T>(CommonDbContext context) : IGenericRepository<T>
    where T : class
{
    protected readonly DbSet<T> DbSet = context.Set<T>();
    protected readonly CommonDbContext Context = context; 

    public async Task<IEnumerable<T>?> GetAllAsync() => await DbSet.AsNoTracking().ToListAsync();

    public async Task AddAsync(T entity) => await DbSet.AddAsync(entity);
    
    public void DeleteAsync(T entity) => DbSet.Remove(entity); 

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await DbSet.AsNoTracking().Where(predicate).ToListAsync();

    public void RemoveRange(IEnumerable<T> entities) => DbSet.RemoveRange(entities); 

    public IQueryable<T> GetQueryableAsync() => DbSet.AsNoTracking().AsQueryable();
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => Context.SaveChangesAsync(cancellationToken);
    
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        => await Context.Database.BeginTransactionAsync(cancellationToken);

    public void ClearChanges() => Context.ClearChanges();
}