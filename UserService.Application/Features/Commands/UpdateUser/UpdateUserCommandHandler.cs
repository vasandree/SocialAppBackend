using MediatR;
using UserService.Application.Features.Commands.UpdateTelegramUser;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IMediator _mediator;

    public UpdateUserCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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