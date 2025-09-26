using Microsoft.EntityFrameworkCore;
using NotificationModule.Domain;
using Shared.DataAccess.Implementation;

namespace NotificationModule.DataAccess.Implementation;

public class NotificationDbContext(DbContextOptions<NotificationDbContext> options) : CommonDbContext(options)
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OutboxMessage>()
            .Property(p => p.Type)
            .HasConversion<string>();
    }
}