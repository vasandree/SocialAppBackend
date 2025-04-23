using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class PersonRepository : SocialNodeRepository<PersonEntity>, IPersonRepository
{
    public PersonRepository(PersosnsDbContext context) : base(context)
    {
    }
}