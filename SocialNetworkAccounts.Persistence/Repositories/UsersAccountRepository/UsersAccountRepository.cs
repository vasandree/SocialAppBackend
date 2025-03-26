using Common.GenericRepository;
using Microsoft.EntityFrameworkCore;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Domain.Enums;

namespace SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

public class UsersAccountRepository : GenericRepository<UsersAccount>, IUsersAccountRepository
{
    private readonly SocialNetworkAccountsDbContext _context;

    public UsersAccountRepository(SocialNetworkAccountsDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<UsersAccount>> GetAllByUserId(Guid userId)
    {
        return await _context.UsersAccounts.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<bool> CheckIfAccountAddedByIdAsync(Guid id)
    {
        return await _context.UsersAccounts.AnyAsync(x => x.Id == id);
    }

    public async Task<UsersAccount?> GetById(Guid id)
    {
        return await _context.UsersAccounts.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork type)
    {
        return await _context.UsersAccounts.AnyAsync(x => x.UserId == userId && x.Type == type);
    }
}