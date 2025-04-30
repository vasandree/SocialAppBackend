using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;
using User.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount;

public class DeleteSocialNetworkAccountCommandHandler : IRequestHandler<DeleteSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;
    private readonly IUserRepository _userRepository;

    public DeleteSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository, IUserRepository userRepository)
    {
        _usersAccountRepository = usersAccountRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExists(request.UserId))
            throw new BadRequest("User does not exist");

        if (!await _usersAccountRepository.CheckIfAccountAddedByIdAsync(request.SocialNetworkAccountId))
            throw new NotFound($"Account with id={request.SocialNetworkAccountId} not found");

        var account = await _usersAccountRepository.GetById(request.SocialNetworkAccountId);

        if (account != null && account.UserId != request.UserId)
            throw new Forbidden("You are not allowed to delete this account");

        if (account != null) await _usersAccountRepository.DeleteAsync(account);

        return Unit.Value;
    }
}