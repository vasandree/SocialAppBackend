using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation.Repositories;
using Shared.Domain;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.DataAccess.Implementation.Repositories;

public class SocialNetworkUrlsRepository(SocialNetworkAccountsDbContext context)
    : GenericRepository<SocialNetworkUrls>(context), ISocialNetworkUrlsRepository
{
    public async Task<bool> UrlExistsAsync(SocialNetwork socialNetwork)
        => await DbSet.AnyAsync(x => x.Type == socialNetwork);


    public async Task<SocialNetworkUrls?> GetByTypeAsync(SocialNetwork socialNetwork)
        => await DbSet.FirstOrDefaultAsync(x => x.Type == socialNetwork);
}