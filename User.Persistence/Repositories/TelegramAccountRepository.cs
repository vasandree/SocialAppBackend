using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using User.Contracts.Repositories;
using User.Domain.Entities;
using User.Infrastructure;
using NotFound = Common.Exceptions.NotFound;

namespace User.Persistence.Repositories;

public class TelegramAccountRepository : GenericRepository<TelegramAccount>, ITelegramAccountRepository
{
    private readonly UserDbContext _context;

    public TelegramAccountRepository(UserDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfUserExistsByTelegramIdAsync(long telegramId)
    {
        return await _context.TelegramAccounts.AnyAsync(x => x.Id == telegramId);
    }

    public async Task<TelegramAccount> GetByTelegramIdAsync(long telegramId)
    {
        return (await _context.TelegramAccounts.FirstOrDefaultAsync(x => x.Id == telegramId))!;
    }

    public async Task<ApplicationUser> GetUserByTelegramIdAsync(long userId)
    {
        var user = await _context.TelegramAccounts.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == userId);
        return user?.User ?? throw new NotFound($"User with telegram id={userId} does not exist");
    }
}