using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation.Repositories;
using UserModule.DataAccess.Interfaces;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.Domain.Entities;
using NotFound = Shared.Domain.Exceptions.NotFound;

namespace UserModule.DataAccess.Implementation.Repositories;

public class TelegramAccountRepository(UserDbContext context)
    : GenericRepository<TelegramAccount>(context), ITelegramAccountRepository
{
    public async Task<bool> CheckIfUserExistsByTelegramIdAsync(long telegramId)
        => await DbSet.AnyAsync(x => x.Id == telegramId);


    public async Task<TelegramAccount> GetByTelegramIdAsync(long telegramId)
        => (await DbSet.FirstOrDefaultAsync(x => x.Id == telegramId))!;


    public async Task<ApplicationUser> GetUserByTelegramIdAsync(long userId)
    {
        var user = await DbSet.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == userId);
        return user?.User ?? throw new NotFound($"User with telegram id={userId} does not exist");
    }
}