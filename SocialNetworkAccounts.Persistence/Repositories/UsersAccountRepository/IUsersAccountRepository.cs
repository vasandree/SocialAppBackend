using Common.GenericRepository;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Domain.Enums;

namespace SocialNetworkAccounts.Persistence.Repositories.UsersAccountRepository;

public interface IUsersAccountRepository : IGenericRepository<UsersAccount>
{
    Task<List<UsersAccount>> GetAllByUserId(Guid userId);
    Task<bool> CheckIfAccountAddedByIdAsync(Guid id);
    Task<UsersAccount?> GetById(Guid id);
    Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork type);
}