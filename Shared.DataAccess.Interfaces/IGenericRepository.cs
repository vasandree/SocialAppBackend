using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Shared.DataAccess.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>?> GetAllAsync();
    Task AddAsync(T entity);
    void DeleteAsync(T entity);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    void RemoveRangeAsync(IEnumerable<T> entities);
    IQueryable<T> GetQueryableAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    DbContext GetDbContext();

    void ClearChanges();
}