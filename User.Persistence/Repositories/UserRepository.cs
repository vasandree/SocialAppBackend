using Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using User.Contracts.Repositories;
using User.Domain.Entities;
using User.Infrastructure;

namespace User.Persistence.Repositories;

public class UserRepository : BaseEntityRepository<ApplicationUser>, IUserRepository
{
    private readonly UserDbContext _context;


    public UserRepository(UserDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ApplicationUser> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username) ??
               throw new NotFound($"User with username={username} does not exist");
    }

    public async Task<bool> CheckIfUserExistsByIdAsync(Guid id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> CheckIfUserExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username);
    }

    public IQueryable<ApplicationUser> GetAllUsers()
    {
        return _context.Users.AsQueryable();
    }
}