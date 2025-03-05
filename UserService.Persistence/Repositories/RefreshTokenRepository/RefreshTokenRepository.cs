using Common.GenericRepository;
using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Persistence.Repositories.RefreshTokenRepository;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    private readonly UserServiceDbContext _context;

    public RefreshTokenRepository(UserServiceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<RefreshToken>> GetByUserIdAsync(Guid userId)
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
}