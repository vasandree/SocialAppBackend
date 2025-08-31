using Shared.DataAccess.Interfaces;
using Shared.Domain;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;

public interface IUsersAccountRepository : IGenericRepository<UsersAccount>
{
    Task<List<UsersAccount>> GetAllByUserId(Guid userId);
    Task<bool> CheckIfAccountAddedByIdAsync(Guid id);
    Task<UsersAccount?> GetById(Guid id);
    Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork type);
    Task<UsersAccount?> GetByUserIdAndTypeAsync(Guid userId, SocialNetwork type);
}