using Auth.Contracts.Commands;
using Auth.Contracts.Repositories;
using Auth.Contracts.Responses;
using Auth.Domain.Entites;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.Domain.Exceptions;
using User.Contracts.Helpers;
using User.Contracts.Repositories;

namespace Auth.Application.Features.Commands;

public class CreateTokensCommandHandler : IRequestHandler<CreateTokensCommand, TokensDto>
{
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public CreateTokensCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository,
        IJwtService jwtService, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    public async Task<TokensDto> Handle(CreateTokensCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExists(request.User.Id))
            throw new BadRequest("User does not exist");

        var refreshToken = new RefreshToken
        {
            Token = _jwtService.GenerateRefreshTokenString(),
            UserId = request.User.Id,
            Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("Jwt:RefreshDaysLifeTime")),
        };
        
        await _refreshTokenRepository.AddAsync(refreshToken);

        return new TokensDto
        {
            AccessToken = _jwtService.GenerateTokenString(request.User.UserName, request.User.Id),
            RefreshToken = refreshToken.Token
        };
    }
}