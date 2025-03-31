using Common.Repositories.BaseEntityRepository;
using UserService.Domain.Entities;

namespace UserService.Persistence.Repositories.UserRepository;

public interface IUserRepository : IBaseEntityRepository<User>
{ 
    Task<User> GetByUsernameAsync(string username);
    Task<bool> CheckIfUserExistsByUsernameAsync(string username);
    IQueryable<User> GetAllUsers();
}