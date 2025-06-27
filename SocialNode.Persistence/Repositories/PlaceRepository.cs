using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class PlaceRepository : BaseSocialNodeRepository<Place>, IPlaceRepository
{
    public PlaceRepository(SocialNodeDbContext context) : base(context)
    {
    }

    public Task<IQueryable<Place>> GetAllByUSerId(Guid userId)
    {
        return Task.FromResult(DbSet.Where(x => x.CreatorId == userId).AsQueryable());
    }
}