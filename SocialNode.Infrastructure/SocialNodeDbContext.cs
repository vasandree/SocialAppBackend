using Microsoft.EntityFrameworkCore;
using SocialNode.Domain.Entities;

namespace SocialNode.Infrastructure;

public class SocialNodeDbContext : DbContext
{
    public SocialNodeDbContext(DbContextOptions<SocialNodeDbContext> options) : base(options)
    {
    }

    private DbSet<PersonEntity> Persons { get; set; }
    private DbSet<Place> Places { get; set; }
    private DbSet<ClusterOfPeople> ClustersOfPeople { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}