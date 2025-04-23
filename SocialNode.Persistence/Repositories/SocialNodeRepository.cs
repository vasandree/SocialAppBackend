using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class SocialNodeRepository<T> : BaseEntityRepository<T>, ISocialNodeRepository<T> where T : BaseSocialNode
{
    private readonly PersosnsDbContext _context;

    public SocialNodeRepository(PersosnsDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<IQueryable<T>> GetAllAsQueryable(Guid userId)
    {
        return Task.FromResult(_context.Set<T>().Where(x => x.CreatorId == userId).AsNoTracking()
            .AsQueryable());
    }

    public async Task<bool> CheckIfUserIsCreator(Guid userId, Guid nodeId)
    {
        return await _context.Set<T>().AnyAsync(x => x.Id == nodeId && x.CreatorId == userId);
    }
}