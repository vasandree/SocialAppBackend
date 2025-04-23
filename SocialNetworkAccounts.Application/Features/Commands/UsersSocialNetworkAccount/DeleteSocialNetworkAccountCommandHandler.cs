using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount;

public class DeleteSocialNetworkAccountCommandHandler : IRequestHandler<DeleteSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;

    public DeleteSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository)
    {
        _usersAccountRepository = usersAccountRepository;
    }

    public async Task<Unit> Handle(DeleteSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence

        if (!await _usersAccountRepository.CheckIfAccountAddedByIdAsync(request.SocialNetworkAccountId))
            throw new NotFound($"Account with id={request.SocialNetworkAccountId} not found");

        var account = await _usersAccountRepository.GetById(request.SocialNetworkAccountId);

        if (account != null && account.UserId != request.UserId)
            throw new Forbidden("You are not allowed to delete this account");

        if (account != null) await _usersAccountRepository.DeleteAsync(account);

        return Unit.Value;
    }
}