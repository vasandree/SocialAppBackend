using SocialNode.Domain.Entities;

namespace SocialNode.Contracts.Services;

public interface ISocialNodeService
{
    public  Task<List<BaseSocialNode>> GetAllNodesForUser(Guid userId, CancellationToken cancellationToken);
}