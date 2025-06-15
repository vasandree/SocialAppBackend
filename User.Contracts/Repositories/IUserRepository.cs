using Shared.Contracts.Repositories;
using User.Domain.Entities;

namespace User.Contracts.Repositories;

public interface IUserRepository : IBaseEntityRepository<ApplicationUser>
{
    Task<ApplicationUser> GetByUsernameAsync(string username);
    Task<bool> CheckIfUserExistsByUsernameAsync(string username);
    IQueryable<ApplicationUser> GetAllUsers();
    new Task<ApplicationUser> GetByIdAsync(Guid id);
}