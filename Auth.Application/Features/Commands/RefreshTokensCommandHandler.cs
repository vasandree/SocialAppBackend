using Auth.Contracts.Commands;
using Auth.Contracts.Repositories;
using Auth.Contracts.Responses;
using MediatR;
using Shared.Domain.Exceptions;
using User.Contracts.Helpers;
using User.Contracts.Repositories;

namespace Auth.Application.Features.Commands;

public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, TokensDto>
{
    private readonly ISender _mediator;
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public RefreshTokensCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository,
        ISender mediator, IJwtService jwtService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _mediator = mediator;
        _jwtService = jwtService;
    }

    public async Task<TokensDto> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        var tokenClaims = _jwtService.GetTokenPrincipal(request.Tokens.AccessToken);

        var userIdClaim = tokenClaims?.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new Unauthorized("Invalid or missing UserId in the token.");
        }

        if (!await _userRepository.CheckIfExists(userId))
            throw new NotFound("user does not exist");

        var user = await _userRepository.GetByIdAsync(userId);

        if (!await _refreshTokenRepository.CheckIfRefreshTokenExistsAsync(request.Tokens.RefreshToken) ||
            !await _refreshTokenRepository.IsRefreshTokenValidAsync(request.Tokens.RefreshToken) ||
            !await _refreshTokenRepository.CheckIfRefreshTokenBelongsToUserAsync(user.Id, request.Tokens.RefreshToken))
            throw new BadRequest("Provided refresh token is not valid");

        var refreshTokenEntity = await _refreshTokenRepository.GetTokenAsync(request.Tokens.RefreshToken);

        if(refreshTokenEntity == null) throw new NotFound("refresh token does not exist");
        
        await _refreshTokenRepository.DeleteAsync(refreshTokenEntity);

        return await _mediator.Send(new CreateTokensCommand(user), cancellationToken);
    }
}