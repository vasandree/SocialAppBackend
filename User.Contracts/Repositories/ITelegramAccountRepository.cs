using Shared.Contracts.Repositories;
using User.Domain.Entities;

namespace User.Contracts.Repositories;

public interface ITelegramAccountRepository : IGenericRepository<TelegramAccount>
{
    Task<bool> CheckIfUserExistsByTelegramIdAsync(long telegramId);
    Task<TelegramAccount> GetByTelegramIdAsync(long telegramId);
    Task<ApplicationUser> GetUserByTelegramIdAsync(long userId);
}