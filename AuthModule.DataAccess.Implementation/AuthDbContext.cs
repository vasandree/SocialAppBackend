using AuthModule.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation;

namespace AuthModule.DataAccess.Implementation;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options) : CommonDbContext(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}