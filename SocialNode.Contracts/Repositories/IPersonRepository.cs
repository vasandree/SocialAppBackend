using SocialNode.Domain.Entities;

namespace SocialNode.Contracts.Repositories;

public interface IPersonRepository : IBaseSocialNodeRepository<PersonEntity>
{ 
    Task<IQueryable<PersonEntity>> GetAllByUserId(Guid userId);
}