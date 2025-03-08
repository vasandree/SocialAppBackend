using MediatR;
using UserService.Application.Features.Commands.UpdateTelegramUser;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IMediator _mediator;

    public UpdateUserCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        switch (request.SocialNetwork)
        {
            case SocialNetwork.Telegram:
                await _mediator.Send(new UpdateTelegramUserCommand(request.InitData), cancellationToken);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return Unit.Value;
    }
}