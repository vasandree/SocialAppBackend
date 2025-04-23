using Common;
using Shared.Domain;

namespace Shared.Contracts.Repositories;

public interface IBaseEntityRepository<T> : IGenericRepository<T> where T: BaseEntity
{
    public Task<T?> GetByIdAsync(Guid id);
    public Task<bool> CheckIfExists(Guid id);
}