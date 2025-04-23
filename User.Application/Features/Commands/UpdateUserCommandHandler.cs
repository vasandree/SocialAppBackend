using MediatR;
using Shared.Domain;
using User.Contracts.Commands;
using User.Domain.Entities;
using User.Domain.Enums;

namespace User.Application.Features.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationUser>
{
    private readonly IMediator _mediator;

    public UpdateUserCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ApplicationUser> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        switch (request.SocialNetwork)
        {
            case SocialNetwork.Telegram:
                return await _mediator.Send(new UpdateTelegramUserCommand(request.InitData), cancellationToken);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}