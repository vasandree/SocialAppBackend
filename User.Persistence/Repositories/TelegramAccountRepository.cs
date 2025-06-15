using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using User.Contracts.Repositories;
using User.Domain.Entities;
using User.Infrastructure;
using NotFound = Shared.Domain.Exceptions.NotFound;

namespace User.Persistence.Repositories;

public class TelegramAccountRepository(UserDbContext context)
    : GenericRepository<TelegramAccount>(context), ITelegramAccountRepository
{
    public async Task<bool> CheckIfUserExistsByTelegramIdAsync(long telegramId)
    {
        return await DbSet.AnyAsync(x => x.Id == telegramId);
    }

    public async Task<TelegramAccount> GetByTelegramIdAsync(long telegramId)
    {
        return (await DbSet.FirstOrDefaultAsync(x => x.Id == telegramId))!;
    }

    public async Task<ApplicationUser> GetUserByTelegramIdAsync(long userId)
    {
        var user = await DbSet.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == userId);
        return user?.User ?? throw new NotFound($"User with telegram id={userId} does not exist");
    }
}