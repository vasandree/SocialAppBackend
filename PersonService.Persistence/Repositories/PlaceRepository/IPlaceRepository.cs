using PersonService.Domain.Entities;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Persistence.Repositories.PlaceRepository;

public interface IPlaceRepository : ISocialNodeRepository<Place>;
