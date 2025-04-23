using Microsoft.EntityFrameworkCore;
using SocialNode.Domain.Entities;

namespace SocialNode.Infrastructure;

public class PersosnsDbContext : DbContext
{
    public PersosnsDbContext(DbContextOptions<PersosnsDbContext> options) : base(options)
    {
    }

    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<ClusterOfPeople> ClustersOfPeople { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}