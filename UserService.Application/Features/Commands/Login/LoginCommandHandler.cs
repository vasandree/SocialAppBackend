using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Features.Commands.LoginWithTelegram;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.Login;

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
            SocialNetwork.Telegram => await _mediator.Send(new LoginWithTelegramCommand(request.InitData),
                cancellationToken),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}