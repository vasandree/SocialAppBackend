using Shared.DataAccess.Interfaces;
using UserModule.Domain.Entities;

namespace UserModule.DataAccess.Interfaces.Repositories;

public interface IUserSettingsRepository : IBaseEntityRepository<UserSettings>
{
    public Task<UserSettings> GetByUserIdAsync(Guid userId);
}