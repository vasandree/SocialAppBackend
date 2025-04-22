using Auth.Contracts.Commands;
using Auth.Contracts.Responses;
using Common.Exceptions;
using MediatR;
using User.Domain.Enums;

namespace Auth.Application.Features.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, TokensDto>
{
    private ISender _mediator;

    public LoginCommandHandler(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<TokensDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return request.Type switch
        {
            SocialNetwork.Telegram => await _mediator.Send(new LoginWithTelegramCommand(request.InitData),
                cancellationToken),
            _ => throw new BadRequest("Provided social network is not supported yet")
        };
    }
}