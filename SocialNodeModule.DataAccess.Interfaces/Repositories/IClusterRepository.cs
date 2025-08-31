using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.DataAccess.Interfaces.Repositories;

public interface IClusterRepository : IBaseSocialNodeRepository<ClusterOfPeople>
{
    Task<IQueryable<ClusterOfPeople>> GetAllByUserId(Guid userId);
}