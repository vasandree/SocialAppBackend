using Shared.DataAccess.Interfaces;
using UserModule.Domain.Entities;

namespace UserModule.DataAccess.Interfaces.Repositories;

public interface IUserRepository : IBaseEntityRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetByUsernameAsync(string username);
    Task<bool> CheckIfUserExistsByUsernameAsync(string username);
    IQueryable<ApplicationUser> GetAllUsers();
    new Task<ApplicationUser> GetByIdAsync(Guid id);
    Task<ApplicationUser?> GetByEmailAsync(string email);
}