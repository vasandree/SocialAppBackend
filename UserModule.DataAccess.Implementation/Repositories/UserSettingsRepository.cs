using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Implementation.Repositories;
using UserModule.DataAccess.Interfaces;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.Domain.Entities;

namespace UserModule.DataAccess.Implementation.Repositories;

public class UserSettingsRepository(UserDbContext context)
    : BaseEntityRepository<UserSettings>(context), IUserSettingsRepository
{
    public async Task<UserSettings> GetByUserIdAsync(Guid userId)
        => await DbSet.FirstOrDefaultAsync(x => x.UserId == userId) ?? throw new InvalidOperationException();
}