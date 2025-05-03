using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Domain;
using SocialNetworkAccounts.Domain.Entities;

namespace SocialNetworkAccounts.Infrastructure;

public class SocialNetworkAccountsDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    private DbSet<PersonsAccount> PersonsAccounts { get; set; }
    private DbSet<UsersAccount> UsersAccounts { get; set; }
    private DbSet<SocialNetworkUrls> SocialNetworkUrls { get; set; }

    public SocialNetworkAccountsDbContext(DbContextOptions<SocialNetworkAccountsDbContext> options,
        IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonsAccount>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<UsersAccount>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<SocialNetworkUrls>()
            .HasKey(x => x.Type);
    }
}