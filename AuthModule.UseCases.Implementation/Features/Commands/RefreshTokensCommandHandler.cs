using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.DataAccess.Interfaces;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;
using Shared.Domain.Exceptions;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.UseCases.Interfaces.Helpers;

namespace AuthModule.UseCases.Implementation.Features.Commands;

internal sealed class RefreshTokensCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    ISender mediator,
    IJwtService jwtService)
    : IRequestHandler<RefreshTokensCommand, TokensDto>
{
    public async Task<TokensDto> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        await using var transaction =
            await userRepository.BeginTransactionAsync(cancellationToken);

        try
        {
            var tokenClaims = jwtService.GetTokenPrincipal(request.Tokens.AccessToken);

            var userIdClaim = tokenClaims?.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new Unauthorized("Invalid or missing UserId in the token.");
            }

            if (!await userRepository.CheckIfExists(userId))
                throw new NotFound("user does not exist");

            var user = await userRepository.GetByIdAsync(userId);

            var refreshToken = await refreshTokenRepository.GetTokenAsync(request.Tokens.RefreshToken)
                               ?? throw new NotFound("refresh token does not exist");

            if (refreshToken.CheckExpired())
                throw new BadRequest("Refresh token has expired");

            if (!refreshToken.IsOwnedByUser(userId))
                throw new BadRequest("Refresh token does not belong to the user");

            var refreshTokenEntity = await refreshTokenRepository.GetTokenAsync(request.Tokens.RefreshToken);

            if (refreshTokenEntity == null) throw new NotFound("refresh token does not exist");

            refreshTokenRepository.DeleteAsync(refreshTokenEntity);

            await userRepository.SaveChangesAsync(cancellationToken);
            await refreshTokenRepository.SaveChangesAsync(cancellationToken);

            var tokens = await mediator.Send(new CreateTokensCommand(user), cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return tokens;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}