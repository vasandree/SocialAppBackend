using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;
using User.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount;

public class EditSocialNetworkAccountCommandHandler : IRequestHandler<EditSocialNetworkAccountCommand, Unit>
{
    private readonly IPersonsAccountRepository _personsAccountRepository;
    private readonly IUserRepository _userRepository;

    public EditSocialNetworkAccountCommandHandler(IPersonsAccountRepository personsAccountRepository,
        IUserRepository userRepository)
    {
        _personsAccountRepository = personsAccountRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(EditSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        
        if (!await _personsAccountRepository.CheckIfAccountAddedByIdAsync(request.AccountId))
            throw new NotFound($"Account with id={request.AccountId} not found");
        
        var account = await _personsAccountRepository.GetById(request.AccountId);

        if (account.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit");

        account.Username = request.EditSocialNetworkAccountDto.Username;

        await _personsAccountRepository.UpdateAsync(account);

        return Unit.Value;
    }
}