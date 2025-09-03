using AuthModule.UseCases.Interfaces.Commands;
using AuthModule.DataAccess.Interfaces;
using AuthModule.Domain.Entites;
using AuthModule.UseCases.Interfaces.Dtos.Responses;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Domain.Exceptions;
using UserModule.DataAccess.Interfaces.Repositories;
using UserModule.UseCases.Interfaces.Helpers;

namespace AuthModule.UseCases.Implementation.Features.Commands;

internal sealed class CreateTokensCommandHandler(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IJwtService jwtService,
    IConfiguration configuration)
    : IRequestHandler<CreateTokensCommand, TokensDto>
{
    private readonly int _expiresIn = configuration.GetValue<int>("Jwt:RefreshDaysLifeTime");

    public async Task<TokensDto> Handle(CreateTokensCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.CheckIfExists(request.User.Id))
            throw new BadRequest("User does not exist");

        var refreshToken = new RefreshToken(request.User.Id, jwtService.GenerateRefreshTokenString(), DateTime.UtcNow.AddHours(_expiresIn));
        
        await refreshTokenRepository.AddAsync(refreshToken);
        
        await refreshTokenRepository.SaveChangesAsync(cancellationToken);

        return new TokensDto(jwtService.GenerateTokenString(request.User.UserName, request.User.Id), refreshToken.Token);
    }
}