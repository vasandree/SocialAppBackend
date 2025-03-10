using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Persistence;

public class UserServiceDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken?> RefreshTokens { get; set; }
    public DbSet<TelegramAccount> TelegramAccounts { get; set; }
    
    public DbSet<SocialNetworkAccount> SocialNetworkAccounts { get; set; }

    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.SocialNetworkAccounts)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.RefreshTokens)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TelegramAccount>()
            .HasOne(a => a.User)
            .WithOne(u => u.TelegramAccount)
            .HasForeignKey<TelegramAccount>(a => a.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}