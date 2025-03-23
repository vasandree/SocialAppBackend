using Common.GenericRepository;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Persistence.Repositories.SocialNetworkAccountRepository;

public class SocialNetworkAccountRepository : GenericRepository<SocialNetworkAccount>, ISocialNetworkAccountRepository
{
    private readonly UserServiceDbContext _context;

    public SocialNetworkAccountRepository(UserServiceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork socialNetwork)
    {
        return await _context.SocialNetworkAccounts.AnyAsync(x => x.UserId == userId && x.Type == socialNetwork);
    }

    public async Task<bool> CheckIfAccountAddedByIdAsync(Guid id)
    {
        return await _context.SocialNetworkAccounts.AnyAsync(x => x.Id == id);
    }

    public async Task<List<SocialNetworkAccount>> GetAllByUserId(Guid userId)
    {
        return await _context.SocialNetworkAccounts.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<SocialNetworkAccount?> GetByUserIdAndSocialNetworkAsync(Guid userId, SocialNetwork socialNetwork)
    {
        return await _context.SocialNetworkAccounts.FirstOrDefaultAsync(x =>
            x.UserId == userId && x.Type == socialNetwork);
    }

    public async Task<SocialNetworkAccount?> GetById(Guid id)
    {
        return await _context.SocialNetworkAccounts.FirstOrDefaultAsync(x => x.Id == id);
    }
}