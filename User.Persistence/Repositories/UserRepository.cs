using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using Shared.Persistence.Repositories;
using User.Contracts.Repositories;
using User.Domain.Entities;
using User.Infrastructure;

namespace User.Persistence.Repositories;

public class UserRepository(UserDbContext context) : BaseEntityRepository<ApplicationUser>(context), IUserRepository
{
    public async Task<ApplicationUser> GetByUsernameAsync(string username)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.UserName == username) ??
               throw new NotFound($"User with username={username} does not exist");
    }

    public async Task<bool> CheckIfUserExistsByIdAsync(Guid id)
    {
        return await DbSet.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> CheckIfUserExistsByUsernameAsync(string username)
    {
        return await DbSet.AnyAsync(x => x.UserName == username);
    }

    public IQueryable<ApplicationUser> GetAllUsers()
    {
        return DbSet.AsQueryable();
    }

    public new async Task<ApplicationUser> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Include(x => x.UserSettings)
            .Include(x => x.TelegramAccount)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();
    }
}