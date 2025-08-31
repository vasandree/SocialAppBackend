using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.UseCases.Interfaces.Commands.UserSocialNetworkAccount;

namespace SocialNetworkAccountModule.UseCases.Implementation.Features.Commands.UsersSocialNetworkAccount;

internal sealed class DeleteSocialNetworkAccountCommandHandler(
    IUsersAccountRepository usersAccountRepository)
    : IRequestHandler<DeleteSocialNetworkAccountCommand, Unit>
{

    public async Task<Unit> Handle(DeleteSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await usersAccountRepository.CheckIfAccountAddedByIdAsync(request.SocialNetworkAccountId))
            throw new NotFound($"Account with id={request.SocialNetworkAccountId} not found");

        var account = await usersAccountRepository.GetById(request.SocialNetworkAccountId);

        if (account != null && !account.CheckIfUserIdEquals(request.UserId))
            throw new Forbidden("You are not allowed to delete this account");

        if (account != null) usersAccountRepository.DeleteAsync(account);

        return Unit.Value;
    }
}