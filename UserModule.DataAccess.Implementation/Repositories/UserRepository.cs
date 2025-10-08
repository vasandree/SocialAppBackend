using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation.Repositories;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.Domain.Entities;

namespace UserModule.DataAccess.Implementation.Repositories;

public class UserRepository(UserDbContext context) : BaseEntityRepository<ApplicationUser>(context), IUserRepository
{
    public async Task<ApplicationUser?> GetByUsernameAsync(string username)
        => await DbSet.FirstOrDefaultAsync(x => x.UserName == username);


    public async Task<bool> CheckIfUserExistsByIdAsync(Guid id)
        => await DbSet.AnyAsync(u => u.Id == id);


    public async Task<bool> CheckIfUserExistsByUsernameAsync(string username)
        => await DbSet.AnyAsync(x => x.UserName == username);


    public IQueryable<ApplicationUser> GetAllUsers()
        => DbSet.AsQueryable();


    public new async Task<ApplicationUser> GetByIdAsync(Guid id)
        => await DbSet
            .Include(x => x.UserSettings)
            .Include(x => x.TelegramAccount)
            .FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();


    public async Task<ApplicationUser?> GetByEmailAsync(string email)
        => await DbSet.FirstOrDefaultAsync(x => x.Email == email);


    public Task<bool> CheckIfUserExistsByEmailAsync(string email)
        => DbSet.AnyAsync(x => x.Email == email);
}