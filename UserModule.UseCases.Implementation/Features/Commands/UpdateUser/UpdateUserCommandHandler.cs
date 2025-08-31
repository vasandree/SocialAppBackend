using MediatR;
using Shared.Domain;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Commands;

namespace UserModule.UseCases.Implementation.Features.Commands.UpdateUser;

internal sealed class UpdateUserCommandHandler(ISender mediator) : IRequestHandler<UpdateUserCommand, ApplicationUser>
{
    public async Task<ApplicationUser> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return request.SocialNetwork switch
        {
            SocialNetwork.Telegram => await mediator.Send(new UpdateTelegramUserCommand(request.InitData),
                cancellationToken),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}