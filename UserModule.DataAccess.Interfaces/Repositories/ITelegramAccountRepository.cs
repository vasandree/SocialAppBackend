using Shared.DataAccess.Interfaces;
using UserModule.Domain.Entities;

namespace UserModule.DataAccess.Interfaces.Repositories;

public interface ITelegramAccountRepository : IGenericRepository<TelegramAccount>
{
    Task<bool> CheckIfUserExistsByTelegramIdAsync(long telegramId);
    Task<TelegramAccount> GetByTelegramIdAsync(long telegramId);
    Task<ApplicationUser> GetUserByTelegramIdAsync(long userId);
}