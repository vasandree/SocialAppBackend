using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class BaseSocialNodeRepository<T> : BaseEntityRepository<T>, IBaseSocialNodeRepository<T> where T: BaseSocialNode
{

    public BaseSocialNodeRepository(SocialNodeDbContext context) : base(context)
    {
    }

    public Task<IQueryable<BaseSocialNode>> GetAllAsQueryable(Guid userId)
    {
        var persons = Context.Set<PersonEntity>().Where(x => x.CreatorId == userId).AsNoTracking();
        var places = Context.Set<Place>().Where(x => x.CreatorId == userId).AsNoTracking();
        var clusters = Context.Set<ClusterOfPeople>().Where(x => x.CreatorId == userId).AsNoTracking();

        var result = persons.Cast<BaseSocialNode>()
            .Concat(places.Cast<BaseSocialNode>())
            .Concat(clusters.Cast<BaseSocialNode>())
            .AsQueryable();

        return Task.FromResult(result);
    }

    public async Task<bool> CheckIfUserIsCreator(Guid userId, Guid nodeId)
    {
        var personExists = await Context.Set<PersonEntity>()
            .AnyAsync(x => x.Id == nodeId && x.CreatorId == userId);

        var placeExists = await Context.Set<Place>()
            .AnyAsync(x => x.Id == nodeId && x.CreatorId == userId);

        var clusterExists = await Context.Set<ClusterOfPeople>()
            .AnyAsync(x => x.Id == nodeId && x.CreatorId == userId);

        return personExists || placeExists || clusterExists;
    }

    public new async Task<bool> CheckIfExists(Guid nodeId)
    {
        var personExists = await Context.Set<PersonEntity>().AnyAsync(x => x.Id == nodeId);
        var placeExists = await Context.Set<Place>().AnyAsync(x => x.Id == nodeId);
        var clusterExists = await Context.Set<ClusterOfPeople>().AnyAsync(x => x.Id == nodeId);

        return personExists || placeExists || clusterExists;
    }
}