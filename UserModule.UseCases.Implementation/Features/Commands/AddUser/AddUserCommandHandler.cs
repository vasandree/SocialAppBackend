using MediatR;
using UserModule.Domain.Entities;
using UserModule.UseCases.Implementation.Features.Commands.AddUser.Factory;
using UserModule.UseCases.Interfaces.Commands;

namespace UserModule.UseCases.Implementation.Features.Commands.AddUser;

internal sealed class AddUserCommandHandler(ISender sender, IAddUserCommandFactory addUserCommandFactory)
    : IRequestHandler<AddUserCommand, ApplicationUser>
{
    public async Task<ApplicationUser> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var command = addUserCommandFactory.Create(request.SocialNetwork, request.InitData);
        return await sender.Send(command, cancellationToken);
    }
}