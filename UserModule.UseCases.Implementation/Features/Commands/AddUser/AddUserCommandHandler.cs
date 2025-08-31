using MediatR;
using Shared.Domain;
using UserModule.Domain.Entities;
using UserModule.UseCases.Interfaces.Commands;

namespace UserModule.UseCases.Implementation.Features.Commands.AddUser;

internal sealed class AddUserCommandHandler(ISender mediator) : IRequestHandler<AddUserCommand, ApplicationUser>
{
    public async Task<ApplicationUser> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        switch (request.SocialNetwork)
        {
            case SocialNetwork.Telegram:
                return await mediator.Send(new AddTelegramUserCommand(request.InitData), cancellationToken);
        }
        //todo: add fabric
        return null;
    }
}