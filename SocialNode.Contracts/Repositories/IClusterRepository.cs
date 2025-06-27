using SocialNode.Domain.Entities;

namespace SocialNode.Contracts.Repositories;

public interface IClusterRepository : IBaseSocialNodeRepository<ClusterOfPeople>
{
    Task<IQueryable<ClusterOfPeople>> GetAllByUserId(Guid userId);
}