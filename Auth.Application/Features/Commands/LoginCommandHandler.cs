using Auth.Contracts.Commands;
using Auth.Contracts.Responses;
using MediatR;
using Shared.Domain;
using Shared.Domain.Exceptions;

namespace Auth.Application.Features.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly ISender _mediator;

    public LoginCommandHandler(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return request.Type switch
        {
            SocialNetwork.Telegram => await _mediator.Send(new LoginWithTelegramCommand(request.InitData),
                cancellationToken),
            _ => throw new BadRequest("Provided social network is not supported yet")
        };
    }
}