using Auth.Domain.Entites;
using Microsoft.EntityFrameworkCore;


namespace Auth.Infrastructure;

public class AuthDbContext : DbContext
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }
}