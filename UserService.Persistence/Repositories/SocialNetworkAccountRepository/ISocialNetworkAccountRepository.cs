using Common.GenericRepository;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Persistence.Repositories.SocialNetworkAccountRepository;

public interface ISocialNetworkAccountRepository : IGenericRepository<SocialNetworkAccount>
{
    Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork socialNetwork);
    Task<bool> CheckIfAccountAddedByIdAsync(Guid id);
    Task<List<SocialNetworkAccount>> GetAllByUserId(Guid userId);
    Task<SocialNetworkAccount?> GetByUserIdAndSocialNetworkAsync(Guid userId, SocialNetwork socialNetwork);
    Task<SocialNetworkAccount?> GetById(Guid id);
}