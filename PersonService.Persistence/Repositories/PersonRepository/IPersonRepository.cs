using PersonService.Domain.Entities;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Persistence.Repositories.PersonRepository;

public interface IPersonRepository : ISocialNodeRepository<Person>;