using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation.Repositories;
using Shared.Domain;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.DataAccess.Implementation.Repositories;

public class PersonsAccountRepository(SocialNetworkAccountsDbContext context)
    : GenericRepository<PersonsAccount>(context), IPersonsAccountRepository
{
    public async Task<bool> CheckIfAccountIsAddedAsync(Guid personsId, SocialNetwork type)
        => await DbSet.AnyAsync(x => x.PersonsId == personsId && x.Type == type);


    public async Task<bool> CheckIfAccountAddedByIdAsync(Guid accountId)
        => await DbSet.AnyAsync(x => x.Id == accountId);


    public async Task<PersonsAccount?> GetById(Guid accountId)
        => await DbSet.FirstOrDefaultAsync(x => x.Id == accountId);
}