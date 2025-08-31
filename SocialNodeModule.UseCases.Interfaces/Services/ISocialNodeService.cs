using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.UseCases.Interfaces.Services;

public interface ISocialNodeService
{
    public  Task<List<BaseSocialNode>> GetAllNodesForUser(Guid userId, CancellationToken cancellationToken);
}