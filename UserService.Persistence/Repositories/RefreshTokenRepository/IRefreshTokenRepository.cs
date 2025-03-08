using Common.GenericRepository;
using UserService.Domain;
using UserService.Domain.Entities;

namespace UserService.Persistence.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<List<RefreshToken>> GetByUserIdAsync(Guid userId);
    Task<bool> IsRefreshTokenValidAsync(string refreshToken);
    Task<bool> CheckIfRefreshTokenBelongsToUserAsync(Guid userId, string refreshToken);
    Task<bool> CheckIfRefreshTokenExistsAsync(string refreshToken);
}