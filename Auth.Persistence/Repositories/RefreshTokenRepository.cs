using Auth.Contracts.Repositories;
using Auth.Domain.Entites;
using Auth.Infrastructure;
using Common.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    private readonly AuthDbContext _context;

    public RefreshTokenRepository(AuthDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<RefreshToken?>> GetByUserIdAsync(Guid userId)
    {
        return await _context.RefreshTokens.Where(x=>x.UserId == userId).ToListAsync();
    }

    public async Task<bool> IsRefreshTokenValidAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(x=>x.Token == refreshToken);
        return token != null && DateTime.UtcNow < token.Expires;
    }

    public async Task<bool> CheckIfRefreshTokenBelongsToUserAsync(Guid userId, string refreshToken)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(x=>x.UserId == userId && x.Token == refreshToken);
        return token != null && token.UserId == userId;
    }

    public async Task<bool> CheckIfRefreshTokenExistsAsync(string refreshToken)
    {
        return await _context.RefreshTokens.AnyAsync(x=>x.Token == refreshToken);
    }

    public async Task<RefreshToken?> GetTokenAsync(string refreshToken)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(x=>x.Token == refreshToken);
    }
}