using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shared.DataAccess.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>?> GetAllAsync();
    Task AddAsync(T entity);
    void DeleteAsync(T entity);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    void RemoveRange(IEnumerable<T> entities);
    IQueryable<T> GetQueryableAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    void ClearChanges();
}