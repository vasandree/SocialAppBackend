using PersonService.Domain.Entities;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Persistence.Repositories.PersonRepository;

public class PersonRepository : SocialNodeRepository<Person>, IPersonRepository
{
    public PersonRepository(PersosnsDbContext context) : base(context.Set<Person>(), context)
    {
    }
}