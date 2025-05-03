using Shared.Contracts.Repositories;
using Shared.Domain;
using SocialNetworkAccounts.Domain.Entities;

namespace SocialNetworkAccounts.Contracts.Repositories;

public interface ISocialNetworkUrlsRepository : IGenericRepository<SocialNetworkUrls>
{
    Task<bool> UrlExistsAsync(SocialNetwork socialNetwork);
    Task<SocialNetworkUrls?> GetByTypeAsync(SocialNetwork socialNetwork);
}