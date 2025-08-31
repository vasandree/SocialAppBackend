using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation;
using UserModule.DataAccess.Interfaces;
using UserModule.Domain.Entities;

namespace UserModule.DataAccess.Implementation;

public sealed class UserDbContext(DbContextOptions<UserDbContext> options) : CommonDbContext(options)
{
    public DbSet<ApplicationUser> Users { get; set; }

    public DbSet<TelegramAccount> TelegramAccounts { get; set; }

    public DbSet<UserSettings> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TelegramAccount>()
            .HasOne(a => a.User)
            .WithOne(u => u.TelegramAccount)
            .HasForeignKey<TelegramAccount>(a => a.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserSettings>()
            .HasOne(u => u.User)
            .WithOne(s => s.UserSettings)
            .HasForeignKey<UserSettings>(s => s.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}