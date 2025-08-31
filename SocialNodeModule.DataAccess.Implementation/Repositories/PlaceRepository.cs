using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.DataAccess.Implementation.Repositories;

public class PlaceRepository(SocialNodeDbContext context) : BaseSocialNodeRepository<Place>(context), IPlaceRepository
{
    public Task<IQueryable<Place>> GetAllByUSerId(Guid userId)
    {
        return Task.FromResult(DbSet.Where(x => x.CreatorId == userId).AsQueryable());
    }
}