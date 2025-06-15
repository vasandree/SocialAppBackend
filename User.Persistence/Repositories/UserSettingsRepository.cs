using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;
using User.Contracts.Repositories;
using User.Domain.Entities;
using User.Infrastructure;

namespace User.Persistence.Repositories;

public class UserSettingsRepository(UserDbContext context)
    : BaseEntityRepository<UserSettings>(context), IUserSettingsRepository
{
    public async Task<UserSettings> GetByUserIdAsync(Guid userId)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.UserId == userId) ?? throw new InvalidOperationException();
    }
}