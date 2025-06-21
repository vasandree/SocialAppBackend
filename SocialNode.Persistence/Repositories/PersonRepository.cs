using SocialNode.Contracts.Repositories;
using SocialNode.Domain.Entities;
using SocialNode.Infrastructure;

namespace SocialNode.Persistence.Repositories;

public class PersonRepository : BaseSocialNodeRepository<PersonEntity>, IPersonRepository
{
    public PersonRepository(SocialNodeDbContext context) : base(context)
    {
    }
}