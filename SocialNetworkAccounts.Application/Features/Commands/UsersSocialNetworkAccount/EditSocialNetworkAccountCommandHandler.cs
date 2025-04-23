using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount;

public class EditSocialNetworkAccountCommandHandler : IRequestHandler<EditSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;

    public EditSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository)
    {
        _usersAccountRepository = usersAccountRepository;
    }


    public async Task<Unit> Handle(EditSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _usersAccountRepository.CheckIfAccountAddedByIdAsync(request.SocialNetworkAccountId))
            throw new NotFound($"Account with id={request.SocialNetworkAccountId} not found");

        //todo: check user existence
        
        var account = await _usersAccountRepository.GetById(request.SocialNetworkAccountId);

        if (account.UserId != request.UserId) throw new Forbidden("You are not allowed to edit");

        account.Username = request.SocialNetworkAccountDto.Username;

        await _usersAccountRepository.UpdateAsync(account);

        return Unit.Value;
    }
}