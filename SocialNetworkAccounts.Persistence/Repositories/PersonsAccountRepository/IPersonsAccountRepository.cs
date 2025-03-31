using Common.Repositories.GenericRepository;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Domain.Enums;

namespace SocialNetworkAccounts.Persistence.Repositories.PersonsAccountRepository;

public interface IPersonsAccountRepository: IGenericRepository<PersonsAccount>
{
    Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork type);
    Task<bool> CheckIfAccountAddedByIdAsync(Guid accountId);
    Task<PersonsAccount?> GetById(Guid accountId);
}