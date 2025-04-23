using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount;

public class DeleteSocialNetworkAccountCommandHandler: IRequestHandler<DeleteSocialNetworkAccountCommand, Unit>
{
    private readonly IPersonsAccountRepository _personsAccountRepository;

    public DeleteSocialNetworkAccountCommandHandler(IPersonsAccountRepository personsAccountRepository)
    {
        _personsAccountRepository = personsAccountRepository;
    }

    public async Task<Unit> Handle(DeleteSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        //todo: check user existence
        
        if (!await _personsAccountRepository.CheckIfAccountAddedByIdAsync(request.AccountId))
            throw new NotFound($"Account with id={request.AccountId} not found");

        var account = await _personsAccountRepository.GetById(request.AccountId);

        if (account != null && account.CreatorId != request.UserId)
            throw new Forbidden("You are not allowed to delete this account");

        if (account != null) await _personsAccountRepository.DeleteAsync(account);

        return Unit.Value;
    }
}