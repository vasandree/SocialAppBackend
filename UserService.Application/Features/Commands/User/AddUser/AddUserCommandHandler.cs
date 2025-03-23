using MediatR;
using UserService.Application.Features.Commands.User.AddTelegramUser;

namespace UserService.Application.Features.Commands.User.AddUser;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Domain.Entities.User>
{
    private readonly IMediator _mediator;

    public AddUserCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Domain.Entities.User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        switch (request.SocialNetwork)
        {
            case Domain.Enums.SocialNetwork.Telegram:
                return await _mediator.Send(new AddTelegramUserCommand(request.InitData), cancellationToken);
        }

        return null;
    }
}