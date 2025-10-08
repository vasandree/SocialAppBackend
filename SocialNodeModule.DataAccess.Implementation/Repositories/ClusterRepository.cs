using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.DataAccess.Implementation.Repositories;

public class ClusterRepository(SocialNodeDbContext context)
    : BaseSocialNodeRepository<ClusterOfPeople>(context), IClusterRepository
{
    public Task<IQueryable<ClusterOfPeople>> GetAllByUserId(Guid userId)
        => Task.FromResult(DbSet.Where(x => x.CreatorId == userId).AsQueryable());
}