using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Persistence.Repositories;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Infrastructure;

namespace SocialNetworkAccounts.Persistence.Repositories;

public class UsersAccountRepository : GenericRepository<UsersAccount>, IUsersAccountRepository
{
    public UsersAccountRepository(SocialNetworkAccountsDbContext context) : base(context)
    {
    }

    public async Task<List<UsersAccount>> GetAllByUserId(Guid userId)
    {
        return await DbSet.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<bool> CheckIfAccountAddedByIdAsync(Guid id)
    {
        return await DbSet.AnyAsync(x => x.Id == id);
    }

    public async Task<UsersAccount?> GetById(Guid id)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork type)
    {
        return await DbSet.AnyAsync(x => x.UserId == userId && x.Type == type);
    }

    public async Task<UsersAccount?> GetByUserIdAndTypeAsync(Guid userId, SocialNetwork type)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.Type == type);
    }
}