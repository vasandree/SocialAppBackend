using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.DataAccess.Implementation;

public sealed class SocialNetworkAccountsDbContext(
    DbContextOptions<SocialNetworkAccountsDbContext> options)
    : CommonDbContext(options)
{
    public DbSet<PersonsAccount> PersonsAccounts { get; set; }
    public DbSet<UsersAccount> UsersAccounts { get; set; }
    public DbSet<SocialNetworkUrls> SocialNetworkUrls { get; set; }

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