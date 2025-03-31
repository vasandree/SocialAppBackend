using Common.Repositories.GenericRepository;

namespace Common.Repositories.BaseEntityRepository;

public interface IBaseEntityRepository<T> : IGenericRepository<T> where T: BaseEntity
{
    public Task<T?> GetByIdAsync(Guid id);
    public Task<bool> CheckIfExists(Guid id);
}