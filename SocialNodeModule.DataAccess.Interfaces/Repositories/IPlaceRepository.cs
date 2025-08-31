using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.DataAccess.Interfaces.Repositories;

public interface IPlaceRepository : IBaseSocialNodeRepository<Place>
{
    Task<IQueryable<Place>> GetAllByUSerId(Guid userId);
}
