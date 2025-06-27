using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class ClusterRepository : BaseSocialNodeRepository<ClusterOfPeople>, IClusterRepository
{
    public ClusterRepository(SocialNodeDbContext context) : base(context)
    {
    }

    public Task<IQueryable<ClusterOfPeople>> GetAllByUserId(Guid userId)
    {
        return Task.FromResult(DbSet.Where(x => x.CreatorId == userId).AsQueryable());
    }
}