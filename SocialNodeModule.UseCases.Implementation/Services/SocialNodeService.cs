using Microsoft.EntityFrameworkCore;
using SocialNodeModule.UseCases.Interfaces.Services;
using SocialNodeModule.Domain.Entities;
using SocialNodeModule.DataAccess.Implementation;

namespace SocialNodeModule.UseCases.Implementation.Services;

public class SocialNodeService(SocialNodeDbContext context) : ISocialNodeService
{
    public async Task<List<BaseSocialNode>> GetAllNodesForUser(Guid userId, CancellationToken cancellationToken)
    {
        var clusters = await context.Set<ClusterOfPeople>()
            .Where(x => x.CreatorId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var persons = await context.Set<PersonEntity>()
            .Where(x => x.CreatorId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var places = await context.Set<Place>()
            .Where(x => x.CreatorId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return clusters.Cast<BaseSocialNode>()
            .Concat(persons)
            .Concat(places)
            .ToList();
    }

}