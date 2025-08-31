using AuthModule.Domain.Entites;
using Shared.DataAccess.Interfaces;

namespace AuthModule.DataAccess.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetTokenAsync(string token);
}