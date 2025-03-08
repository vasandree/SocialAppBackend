using Common.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Features.Commands.CreateTokens;
using UserService.Persistence.Repositories.RefreshTokenRepository;
using UserService.Persistence.Repositories.UserRepository;

namespace UserService.Application.Features.Commands.RefreshTokens;

public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, TokensDto>
{
    private readonly IMediator _mediator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public RefreshTokensCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IMediator mediator)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<TokensDto> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfUserExistsByIdAsync(request.UserId))
            throw new BadRequest("user does not exist");

        var user = await _userRepository.GetUserByIdAsync(request.UserId);

        if (!await _refreshTokenRepository.CheckIfRefreshTokenExistsAsync(request.RefreshToken) ||
            !await _refreshTokenRepository.IsRefreshTokenValidAsync(request.RefreshToken) ||
            !await _refreshTokenRepository.CheckIfRefreshTokenBelongsToUserAsync(user.Id, request.RefreshToken))
            throw new BadRequest("Provided refresh token is not valid");

        var refreshTokenEntity = _refreshTokenRepository.FindAsync(x => x.Token == request.RefreshToken).Result.First();
        
        await _refreshTokenRepository.DeleteAsync(refreshTokenEntity);

        return await _mediator.Send(new CreateTokensCommand(user), cancellationToken);
    }
}