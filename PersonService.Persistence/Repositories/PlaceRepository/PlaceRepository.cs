using PersonService.Domain.Entities;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Persistence.Repositories.PlaceRepository;

public class PlaceRepository : SocialNodeRepository<Place>, IPlaceRepository
{
    public PlaceRepository(PersosnsDbContext context) : base(context.Set<Place>(), context)
    {
    }
}