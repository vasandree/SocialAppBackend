using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class SocialNodeRepository<T> : BaseEntityRepository<T>, ISocialNodeRepository<T> where T : BaseSocialNode
{

    public SocialNodeRepository(SocialNodeDbContext context) : base(context)
    {
    }

    public Task<IQueryable<T>> GetAllAsQueryable(Guid userId)
    {
        return Task.FromResult(DbSet.Where(x => x.CreatorId == userId).AsNoTracking()
            .AsQueryable());
    }

    public async Task<bool> CheckIfUserIsCreator(Guid userId, Guid nodeId)
    {
        return await DbSet.AnyAsync(x => x.Id == nodeId && x.CreatorId == userId);
    }
}