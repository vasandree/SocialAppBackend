using Common.GenericRepository;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Persistence.Repositories.TelegramAccountRepository;

public class TelegramAccountRepository : GenericRepository<TelegramAccount>, ITelegramAccountRepository
{
    private readonly UserServiceDbContext _context;

    public TelegramAccountRepository(UserServiceDbContext context) : base(context)
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

    public async Task<User> GetUserByTelegramIdAsync(long telegramId)
    {
        return (await _context.Users
            .Join(
                _context.TelegramAccounts,
                user => user.Id,
                account => account.UserId,
                (user, account) => new { User = user, Account = account })
            .Where(x => x.Account.Id == telegramId)
            .Select(x => x.User)
            .Include(x => x.SocialNetworkAccounts)
            .FirstOrDefaultAsync().ConfigureAwait(false))!;
    }
}