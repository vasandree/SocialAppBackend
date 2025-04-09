using PersonService.Domain.Entities;
using PersonService.Persistence.Repositories.SocialNodeRepository;

namespace PersonService.Persistence.Repositories.ClusterRepository;

public interface IClusterRepository : ISocialNodeRepository<ClusterOfPeople>;