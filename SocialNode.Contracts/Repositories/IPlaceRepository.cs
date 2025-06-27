using SocialNode.Domain.Entities;

namespace SocialNode.Contracts.Repositories;

public interface IPlaceRepository : IBaseSocialNodeRepository<Place>
{
    Task<IQueryable<Place>> GetAllByUSerId(Guid userId);
}
