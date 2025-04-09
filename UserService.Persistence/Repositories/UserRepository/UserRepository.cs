using Common;
using Common.Exceptions;
using Common.Repositories.BaseEntityRepository;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Persistence.Repositories.UserRepository;

public class UserRepository : BaseEntityRepository<User>, IUserRepository
{
    private readonly UserServiceDbContext _context;


    public UserRepository( DbSet<User> dbSet, UserServiceDbContext context) : base(context, dbSet)
    {
        _context = context;
    }

    public async Task<User> GetByUsernameAsync(string username)
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

    public IQueryable<User> GetAllUsers()
    {
        return _context.Users.AsQueryable();
    }
}