using Common.Repositories.BaseEntityRepository;
using PersonService.Domain.Entities;

namespace PersonService.Persistence.Repositories.SocialNodeRepository;

public interface ISocialNodeRepository<T> : IBaseEntityRepository<T> where T : BaseSocialNode
{
    Task<IQueryable<T>> GetAllAsQueryable(Guid userId);
    public Task<bool> CheckIfUserIsCreator(Guid userId, Guid nodeId);
}