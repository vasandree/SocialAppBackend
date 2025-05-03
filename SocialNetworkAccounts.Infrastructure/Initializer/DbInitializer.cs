using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Domain;
using Shared.Extensions.Configs;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;

namespace SocialNetworkAccounts.Infrastructure.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly SocialNetworkAccountsDbContext _context;
    private readonly Dictionary<string, string> _baseUrls;
    private readonly ISocialNetworkUrlsRepository _repository;

    public DbInitializer(IOptions<SocialNetworkBaseUrlsConfig> config, ISocialNetworkUrlsRepository repository,
        SocialNetworkAccountsDbContext context)
    {
        _baseUrls = config.Value.SocialNetworkBaseUrls;
        _repository = repository;
        _context = context;
    }


    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync();

        foreach (var baseUrl in _baseUrls)
        {
            if (Enum.TryParse<SocialNetwork>(baseUrl.Key, out var socialNetwork))
            {
                if (!await _repository.UrlExistsAsync(socialNetwork))
                {
                    var entity = new SocialNetworkUrls
                    {
                        Type = socialNetwork,
                        Url = baseUrl.Value
                    };

                    await _repository.AddAsync(entity);
                }
                else
                {
                    var entity = await _repository.GetByTypeAsync(socialNetwork);
                    entity!.Url = baseUrl.Value;
                    await _repository.UpdateAsync(entity);
                }
            }
        }
    }
}