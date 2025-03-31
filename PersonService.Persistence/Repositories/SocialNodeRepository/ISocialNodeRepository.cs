using Common.Repositories.BaseEntityRepository;
using PersonService.Domain;

namespace PersonService.Persistence.Repositories.SocialNodeRepository;

public interface ISocialNodeRepository: IBaseEntityRepository<BaseSocialNode>
{
    public Task<IEnumerable<BaseSocialNode>> GetByName(string name, int page, int pageSize, Guid userId);
    public Task<IEnumerable<BaseSocialNode>> GetAll(Guid userId, int page, int pageSize);
}