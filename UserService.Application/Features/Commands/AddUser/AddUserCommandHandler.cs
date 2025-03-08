using MediatR;
using UserService.Application.Features.Commands.AddTelegramUser;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
{
    private readonly IMediator _mediator;

    public AddUserCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        switch (request.SocialNetwork)
        {
            case SocialNetwork.Telegram:
                return await _mediator.Send(new AddTelegramUserCommand(request.InitData), cancellationToken);
        }

        return null;
    }
}