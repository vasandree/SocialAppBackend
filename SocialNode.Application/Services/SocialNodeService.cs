using Microsoft.EntityFrameworkCore;
using SocialNode.Contracts.Services;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Application.Services;

public class SocialNodeService : ISocialNodeService
{
    private readonly SocialNodeDbContext _context;

    public SocialNodeService(SocialNodeDbContext context)
    {
        _context = context;
    }

    public async Task<List<BaseSocialNode>> GetAllNodesForUser(Guid userId, CancellationToken cancellationToken)
    {
        var clusters = await _context.Set<ClusterOfPeople>()
            .Where(x => x.CreatorId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var persons = await _context.Set<PersonEntity>()
            .Where(x => x.CreatorId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var places = await _context.Set<Place>()
            .Where(x => x.CreatorId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return clusters.Cast<BaseSocialNode>()
            .Concat(persons)
            .Concat(places)
            .ToList();
    }

}