using Microsoft.EntityFrameworkCore;
using PersonService.Domain;
using PersonService.Domain.Entities;

namespace PersonService.Persistence;

public class PersosnsDbContext: DbContext
{ 
    public PersosnsDbContext(DbContextOptions<PersosnsDbContext> options) : base(options)
    {
    }
    
    public DbSet<Person> Persons { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<ClusterOfPeople> ClustersOfPeople { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}