using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Domain;
using Shared.Extensions.Configs;
using SocialNetworkAccountModule.DataAccess.Interfaces;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.DataAccess.Implementation.Initializer;

internal class DbInitializer(
    IOptions<SocialNetworkBaseUrlsConfig> config,
    ISocialNetworkUrlsRepository repository,
    SocialNetworkAccountsDbContext context)
    : IDbInitializer
{
    private readonly Dictionary<string, string> _baseUrls = config.Value.SocialNetworkBaseUrls;
    
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await context.Database.MigrateAsync(cancellationToken: cancellationToken);

        foreach (var baseUrl in _baseUrls)
        {
            if (Enum.TryParse<SocialNetwork>(baseUrl.Key, out var socialNetwork))
            {
                if (!await repository.UrlExistsAsync(socialNetwork))
                {
                    var entity = new SocialNetworkUrls(socialNetwork, baseUrl.Value);

                    await repository.AddAsync(entity);
                }
                else
                {
                    var entity = await repository.GetByTypeAsync(socialNetwork);
                    entity?.ChangeUrl(baseUrl.Value);
                    await repository.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}