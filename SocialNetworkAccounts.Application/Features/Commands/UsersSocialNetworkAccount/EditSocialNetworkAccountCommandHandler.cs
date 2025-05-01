using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;
using User.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount;

public class EditSocialNetworkAccountCommandHandler : IRequestHandler<EditSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;
    private readonly IUserRepository _userRepository;

    public EditSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository,
        IUserRepository userRepository)
    {
        _usersAccountRepository = usersAccountRepository;
        _userRepository = userRepository;
    }


    public async Task<Unit> Handle(EditSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfExists(request.UserId))
            throw new BadRequest("User does not exist");

        var account = await _usersAccountRepository.GetById(request.SocialNetworkAccountId);

        if (account.UserId != request.UserId) throw new Forbidden("You are not allowed to edit");

        account.Username = request.SocialNetworkAccountDto.Username;

        await _usersAccountRepository.UpdateAsync(account);

        return Unit.Value;
    }
}