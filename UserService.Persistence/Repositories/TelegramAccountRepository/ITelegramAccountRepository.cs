using Common.GenericRepository;
using UserService.Domain.Entities;

namespace UserService.Persistence.Repositories.TelegramAccountRepository;

public interface ITelegramAccountRepository : IGenericRepository<TelegramAccount>
{
    Task<bool> CheckIfUserExistsByTelegramIdAsync(long telegramId);
    Task<TelegramAccount> GetByTelegramIdAsync(long telegramId);
    Task<User> GetUserByTelegramIdAsync(long telegramId);
}