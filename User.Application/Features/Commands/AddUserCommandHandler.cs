using MediatR;
using User.Contracts.Commands;
using User.Domain.Entities;
using User.Domain.Enums;

namespace User.Application.Features.Commands;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ApplicationUser>
{
    private readonly IMediator _mediator;

    public AddUserCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ApplicationUser> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        switch (request.SocialNetwork)
        {
            case SocialNetwork.Telegram:
                return await _mediator.Send(new AddTelegramUserCommand(request.InitData), cancellationToken);
        }

        return null;
    }
}