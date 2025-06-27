using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class PersonRepository : BaseSocialNodeRepository<PersonEntity>, IPersonRepository
{
    public PersonRepository(SocialNodeDbContext context) : base(context)
    {
    }

    public Task<IQueryable<PersonEntity>> GetAllByUserId(Guid userId)
    {
        return Task.FromResult(DbSet.Where(x=>x.CreatorId == userId).AsQueryable());
    }
}