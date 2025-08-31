using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation;
using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.DataAccess.Implementation;

public sealed class SocialNodeDbContext(DbContextOptions<SocialNodeDbContext> options) : CommonDbContext(options)
{
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<ClusterOfPeople> ClustersOfPeople { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}