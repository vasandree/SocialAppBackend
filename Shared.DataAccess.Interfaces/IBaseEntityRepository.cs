using Shared.Domain;

namespace Shared.DataAccess.Interfaces;

public interface IBaseEntityRepository<T> : IGenericRepository<T> where T: BaseEntity
{
    public Task<T> GetByIdAsync(Guid id);
    public Task<bool> CheckIfExists(Guid id);
}