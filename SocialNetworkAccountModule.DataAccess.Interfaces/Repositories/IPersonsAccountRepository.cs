using Shared.DataAccess.Interfaces;
using Shared.Domain;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;

public interface IPersonsAccountRepository : IGenericRepository<PersonsAccount>
{
    Task<bool> CheckIfAccountIsAddedAsync(Guid userId, SocialNetwork type);
    Task<bool> CheckIfAccountAddedByIdAsync(Guid accountId);
    Task<PersonsAccount?> GetById(Guid accountId);
}