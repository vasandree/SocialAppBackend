using Common.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Features.Commands.Auth.LoginWithTelegram;

namespace UserService.Application.Features.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, TokensDto>
{
    private IMediator _mediator;

    public LoginCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TokensDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return request.Type switch
        {
            Domain.Enums.SocialNetwork.Telegram => await _mediator.Send(new LoginWithTelegramCommand(request.InitData),
                cancellationToken),
            _ => throw new BadRequest("Provided social network is not supported yet")
        };
    }
}