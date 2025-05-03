using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event.Infrastructure;

public class EventDbContext : DbContext
{
    private DbSet<EventEntity> Events { get; set; }
    private DbSet<EventType> EventTypes { get; set; }

    public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventEntity>()
            .HasOne(e => e.EventType)
            .WithMany()
            .HasForeignKey(e => e.EventTypeId);
    }

}