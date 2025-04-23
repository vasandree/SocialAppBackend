using Shared.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Domain.Enums;

namespace SocialNetworkAccounts.Contracts.Repositories;

public interface IPersonsAccountRepository : IGenericRepository<PersonsAccount>
{
    Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork type);
    Task<bool> CheckIfAccountAddedByIdAsync(Guid accountId);
    Task<PersonsAccount?> GetById(Guid accountId);
}