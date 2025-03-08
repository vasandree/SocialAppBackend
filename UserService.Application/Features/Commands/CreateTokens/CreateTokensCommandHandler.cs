using Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using UserService.Application.Dtos.Responses;
using UserService.Application.Helpers.JwtService;
using UserService.Domain;
using UserService.Persistence.Repositories.RefreshTokenRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.CreateTokens;

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
        if (!await _userRepository.CheckIfUserExistsByIdAsync(request.User.Id))
            throw new BadRequest("User does not exist");

        var refreshToken = new RefreshToken
        {
            Token = _jwtService.GenerateRefreshTokenString(),
            UserId = request.User.Id,
            Expires = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshDaysLifeTime")),
        };
        
        await _refreshTokenRepository.AddAsync(refreshToken);

        return new TokensDto
        {
            AccessToken = _jwtService.GenerateTokenString(request.User.UserName, request.User.Id),
            RefreshToken = refreshToken.Token
        };
    }
}