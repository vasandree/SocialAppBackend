using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Persistence;

public class UserServiceDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
    {
    }
}