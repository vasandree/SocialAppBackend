using PersonService.Domain.Entities;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Persistence.Repositories.ClusterRepository;

public class ClusterRepository : SocialNodeRepository<ClusterOfPeople>, IClusterRepository
{
    public ClusterRepository(PersosnsDbContext context) : base(context.Set<ClusterOfPeople>(), context)
    {
    }
}