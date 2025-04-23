using Auth.Domain.Entites;
using Shared.Contracts.Repositories;

namespace Auth.Contracts.Repositories;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<List<RefreshToken?>> GetByUserIdAsync(Guid userId);
    Task<bool> IsRefreshTokenValidAsync(string refreshToken);
    Task<bool> CheckIfRefreshTokenBelongsToUserAsync(Guid userId, string refreshToken);
    Task<bool> CheckIfRefreshTokenExistsAsync(string refreshToken);
    Task<RefreshToken?> GetTokenAsync(string refreshToken);
}