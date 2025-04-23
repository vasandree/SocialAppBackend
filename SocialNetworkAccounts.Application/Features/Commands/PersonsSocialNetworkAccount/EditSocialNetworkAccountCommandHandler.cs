using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount;

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

        //todo: check user existsnce

        var account = await _personsAccountRepository.GetById(request.AccountId);

        if (account.CreatorId != request.UserId) throw new Forbidden("You are not allowed to edit");

        account.Username = request.EditSocialNetworkAccountDto.Username;

        await _personsAccountRepository.UpdateAsync(account);

        return Unit.Value;
    }
}