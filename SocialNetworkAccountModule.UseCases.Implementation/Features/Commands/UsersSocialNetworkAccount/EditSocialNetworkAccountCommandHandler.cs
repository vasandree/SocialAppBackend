using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.UseCases.Interfaces.Commands.UserSocialNetworkAccount;
using UserModule.DataAccess.Interfaces.Repositories;

namespace SocialNetworkAccountModule.UseCases.Implementation.Features.Commands.UsersSocialNetworkAccount;

internal sealed class EditSocialNetworkAccountCommandHandler(
    IUsersAccountRepository usersAccountRepository,
    IUserRepository userRepository)
    : IRequestHandler<EditSocialNetworkAccountCommand, Unit>
{
    public async Task<Unit> Handle(EditSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.CheckIfExists(request.UserId))
            throw new BadRequest("User does not exist");

        var account = await usersAccountRepository.GetById(request.SocialNetworkAccountId);

        if (!account.CheckIfUserIdEquals(request.UserId)) throw new Forbidden("You are not allowed to edit");

        account.UpdateUsername(request.SocialNetworkAccountDto.Username); 

        await usersAccountRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}