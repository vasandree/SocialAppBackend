using SocialNodeModule.Domain.Entities;

namespace SocialNodeModule.DataAccess.Interfaces.Repositories;

public interface IPersonRepository : IBaseSocialNodeRepository<PersonEntity>
{ 
    Task<IQueryable<PersonEntity>> GetAllByUserId(Guid userId);
}