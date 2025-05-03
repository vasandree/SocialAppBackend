using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Persistence.Repositories;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Infrastructure;

namespace SocialNetworkAccounts.Persistence.Repositories;

public class SocialNetworkUrlsRepository : GenericRepository<SocialNetworkUrls>, ISocialNetworkUrlsRepository
{
    public SocialNetworkUrlsRepository(SocialNetworkAccountsDbContext context) : base(context)
    {
    }

    public async Task<bool> UrlExistsAsync(SocialNetwork socialNetwork)
    {
        return await DbSet.AnyAsync(x => x.Type == socialNetwork);
    }

    public async Task<SocialNetworkUrls?> GetByTypeAsync(SocialNetwork socialNetwork)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Type == socialNetwork);
    }
}