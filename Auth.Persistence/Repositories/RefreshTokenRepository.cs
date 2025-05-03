using Auth.Contracts.Repositories;
using Auth.Domain.Entites;
using Auth.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories;

namespace Auth.Persistence.Repositories;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{

    public RefreshTokenRepository(AuthDbContext context) : base(context)
    {
    }

    public async Task<List<RefreshToken?>> GetByUserIdAsync(Guid userId)
    {
        return await DbSet.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<bool> IsRefreshTokenValidAsync(string refreshToken)
    {
        var token = await DbSet.FirstOrDefaultAsync(x => x.Token == refreshToken);
        return token != null && DateTime.UtcNow < token.Expires;
    }

    public async Task<bool> CheckIfRefreshTokenBelongsToUserAsync(Guid userId, string refreshToken)
    {
        var token = await DbSet.FirstOrDefaultAsync(x =>
            x.UserId == userId && x.Token == refreshToken);
        return token != null && token.UserId == userId;
    }

    public async Task<bool> CheckIfRefreshTokenExistsAsync(string refreshToken)
    {
        return await DbSet.AnyAsync(x => x.Token == refreshToken);
    }

    public async Task<RefreshToken?> GetTokenAsync(string refreshToken)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Token == refreshToken);
    }
}