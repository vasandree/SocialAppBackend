using Common.GenericRepository;
using UserService.Domain;

namespace UserService.Persistence.Repositories.UserRepository;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetByTelegramIdAsync(long telegramId);
    Task<User> GetByUsernameAsync(string username);
    Task<bool> CheckIfUserExistsByTelegramIdAsync(long telegramId);
    Task<bool> CheckIfUserExistsByIdAsync(Guid id);
    Task<bool> CheckIfUserExistsByUsernameAsync(string username);
    IQueryable<User> GetAllUsers();
}