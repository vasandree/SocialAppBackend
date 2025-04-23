using Shared.Contracts.Repositories;
using Shared.Domain;
using SocialNetworkAccounts.Domain.Entities;

namespace SocialNetworkAccounts.Contracts.Repositories;

public interface IUsersAccountRepository : IGenericRepository<UsersAccount>
{
    Task<List<UsersAccount>> GetAllByUserId(Guid userId);
    Task<bool> CheckIfAccountAddedByIdAsync(Guid id);
    Task<UsersAccount?> GetById(Guid id);
    Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork type);
}