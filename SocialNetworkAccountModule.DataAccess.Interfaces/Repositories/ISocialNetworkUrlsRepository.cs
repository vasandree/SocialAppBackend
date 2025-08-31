using Shared.DataAccess.Interfaces;
using Shared.Domain;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;

public interface ISocialNetworkUrlsRepository : IGenericRepository<SocialNetworkUrls>
{
    Task<bool> UrlExistsAsync(SocialNetwork socialNetwork);
    Task<SocialNetworkUrls?> GetByTypeAsync(SocialNetwork socialNetwork);
}