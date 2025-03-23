using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Persistence.Repositories.PersonsAccountRepository;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount.EditSocialNetworkAccount;

public class EditSocialNetworkAccountCommandHandler : IRequestHandler<EditSocialNetworkAccountCommand, Unit>
{
    private readonly IPersonsAccountRepository _personsAccountRepository;

    public EditSocialNetworkAccountCommandHandler(IPersonsAccountRepository personsAccountRepository)
    {
        _personsAccountRepository = personsAccountRepository;
    }

    public async Task<Unit> Handle(EditSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await _personsAccountRepository.CheckIfAccountAddedByIdAsync(request.AccountId))
            throw new NotFound($"Account with id={request.AccountId} not found");

        //todo: check user existence

        var account = await _personsAccountRepository.GetById(request.AccountId);

        if (account.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit");

        account.Username = request.EditSocialNetworkAccountDto.Username;

        await _personsAccountRepository.UpdateAsync(account);

        return Unit.Value;
    }
}