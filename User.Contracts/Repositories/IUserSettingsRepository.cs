using Shared.Contracts.Repositories;
using User.Domain.Entities;

namespace User.Contracts.Repositories;

public interface IUserSettingsRepository : IBaseEntityRepository<UserSettings>
{
    public Task<UserSettings> GetByUserIdAsync(Guid userId);
}