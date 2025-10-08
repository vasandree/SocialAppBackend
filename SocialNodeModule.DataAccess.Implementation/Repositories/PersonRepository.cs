using SocialNodeModule.DataAccess.Interfaces.Repositories;
using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.DataAccess.Implementation.Repositories;

public class PersonRepository(SocialNodeDbContext context)
    : BaseSocialNodeRepository<PersonEntity>(context), IPersonRepository
{
    public Task<IQueryable<PersonEntity>> GetAllByUserId(Guid userId)
        => Task.FromResult(DbSet.Where(x => x.CreatorId == userId).AsQueryable());
}