using EventModule.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation;

namespace EventModule.DataAccess.Implementation;

public sealed class EventDbContext(DbContextOptions<EventDbContext> options) : CommonDbContext(options)
{
    
    public DbSet<EventEntity> Events { get; set; }
    public DbSet<EventTypeEntity> EventTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventEntity>()
            .HasOne(e => e.EventType)
            .WithMany()
            .HasForeignKey(e => e.EventTypeId);
    }

}