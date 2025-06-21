using Shared.Contracts.Repositories;
using SocialNode.Domain.Entities;

namespace SocialNode.Contracts.Repositories;

public interface IBaseSocialNodeRepository<T> : IBaseEntityRepository<T> where T : BaseSocialNode
{
    Task<IQueryable<BaseSocialNode>> GetAllAsQueryable(Guid userId);
    public Task<bool> CheckIfUserIsCreator(Guid userId, Guid nodeId);
    public new Task<bool> CheckIfExists(Guid nodeId);
}