using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;

namespace User.Infrastructure;

public class UserDbContext : DbContext
{
    private DbSet<ApplicationUser> Users { get; set; }

    private DbSet<TelegramAccount> TelegramAccounts { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TelegramAccount>()
            .HasOne(a => a.User)
            .WithOne(u => u.TelegramAccount)
            .HasForeignKey<TelegramAccount>(a => a.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}