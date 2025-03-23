using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Domain.Enums;

namespace SocialNetworkAccounts.Persistence;

public class SocialNetworkAccountsDbContext: DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<PersonsAccount> PersonsAccounts { get; set; }
    public DbSet<UsersAccount> UsersAccounts { get; set; }
    public DbSet<SocialNetworkUrls> SocialNetworkUrls { get; set; }

    public SocialNetworkAccountsDbContext(DbContextOptions<SocialNetworkAccountsDbContext> options, IConfiguration configuration) : base(options)
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

        var socialNetworkBaseUrls = _configuration.GetSection("SocialNetworkBaseUrls").Get<Dictionary<string, string>>();

        foreach (var baseUrl in socialNetworkBaseUrls)
        {
            if (Enum.TryParse<SocialNetwork>(baseUrl.Key, out var socialNetwork))
            {
                modelBuilder.Entity<SocialNetworkUrls>().HasData(new SocialNetworkUrls
                {
                    Type = socialNetwork,
                    Url = baseUrl.Value
                });
            }
        }
    }
}
