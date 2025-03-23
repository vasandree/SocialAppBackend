using MediatR;
using UserService.Application.Features.Commands.User.UpdateTelegramUser;

namespace UserService.Application.Features.Commands.User.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Domain.Entities.User>
{
    private readonly IMediator _mediator;

    public UpdateUserCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Domain.Entities.User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        switch (request.SocialNetwork)
        {
            case Domain.Enums.SocialNetwork.Telegram:
                return await _mediator.Send(new UpdateTelegramUserCommand(request.InitData), cancellationToken);
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
}