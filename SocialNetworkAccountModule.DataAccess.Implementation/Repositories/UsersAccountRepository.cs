using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation.Repositories;
using Shared.Domain;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.DataAccess.Implementation.Repositories;

public class UsersAccountRepository(SocialNetworkAccountsDbContext context)
    : GenericRepository<UsersAccount>(context), IUsersAccountRepository
{
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