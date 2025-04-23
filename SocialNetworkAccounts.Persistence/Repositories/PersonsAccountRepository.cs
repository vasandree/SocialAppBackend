using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Persistence.Repositories;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;
using SocialNetworkAccounts.Infrastructure;

namespace SocialNetworkAccounts.Persistence.Repositories;

public class PersonsAccountRepository : GenericRepository<PersonsAccount>, IPersonsAccountRepository
{
    private readonly SocialNetworkAccountsDbContext _context;

    public PersonsAccountRepository(SocialNetworkAccountsDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfAccountIsAddedAsync(Guid personsId, SocialNetwork type)
    {
        return await _context.PersonsAccounts.AnyAsync(x => x.PersonsId == personsId && x.Type == type);
    }

    public async Task<bool> CheckIfAccountAddedByIdAsync(Guid accountId)
    {
        return await _context.PersonsAccounts.AnyAsync(x => x.Id == accountId);
    }

    public async Task<PersonsAccount?> GetById(Guid accountId)
    {
        return await _context.PersonsAccounts.FirstOrDefaultAsync(x => x.Id == accountId);
    }
}