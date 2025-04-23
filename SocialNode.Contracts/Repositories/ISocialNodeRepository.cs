using Shared.Contracts.Repositories;
using SocialNode.Domain.Entities;

namespace SocialNode.Contracts.Repositories;

public interface ISocialNodeRepository<T> : IBaseEntityRepository<T> where T : BaseSocialNode
{
    Task<IQueryable<T>> GetAllAsQueryable(Guid userId);
    public Task<bool> CheckIfUserIsCreator(Guid userId, Guid nodeId);
}