using Common.Exceptions;
using Common.GenericRepository;
using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Persistence.Repositories.UserRepository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly UserServiceDbContext _context;

    public UserRepository(UserServiceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ??
               throw new NotFound($"User with id={id} does not exist");
    }

    public async Task<User> GetByTelegramIdAsync(long telegramId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId)
               ?? throw new NotFound($"User with telegramId={telegramId} does not exist");
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username) ??
               throw new NotFound($"User with username={username} does not exist");
    }

    public async Task<bool> CheckIfUserExistsByTelegramIdAsync(long telegramId)
    {
       return await _context.Users.AnyAsync(x => x.TelegramId == telegramId);
    }

    public async Task<bool> CheckIfUserExistsByIdAsync(Guid id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> CheckIfUserExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username);
    }
}