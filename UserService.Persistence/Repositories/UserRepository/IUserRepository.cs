using Common.GenericRepository;
using UserService.Domain;
using UserService.Domain.Entities;

namespace UserService.Persistence.Repositories.UserRepository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> GetByUsernameAsync(string username);
    Task<bool> CheckIfUserExistsByIdAsync(Guid id);
    Task<bool> CheckIfUserExistsByUsernameAsync(string username);
    IQueryable<User> GetAllUsers();
}