using AuthModule.Domain.Entites;
using AuthModule.DataAccess.Interfaces;
using Shared.DataAccess.Implementation.Repositories;

namespace AuthModule.DataAccess.Implementation.Repositories;

internal class RefreshTokenRepository(AuthDbContext context)
    : GenericRepository<RefreshToken>(context), IRefreshTokenRepository
{
    public Task<RefreshToken?> GetTokenAsync(string token)
    {
        return Task.FromResult(DbSet.FirstOrDefault(x => x.Token == token));
    }
}