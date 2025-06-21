using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class ClusterRepository : BaseSocialNodeRepository<ClusterOfPeople>, IClusterRepository
{
    public ClusterRepository(SocialNodeDbContext context) : base(context)
    {
    }
}