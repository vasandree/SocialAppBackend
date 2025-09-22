using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.UseCases.Interfaces.Commands.PersonSocialNetworkAccount;

namespace SocialNetworkAccountModule.UseCases.Implementation.Features.Commands.PersonsSocialNetworkAccount;

internal sealed class DeleteSocialNetworkAccountCommandHandler(
    IPersonsAccountRepository personsAccountRepository)
    : IRequestHandler<DeleteSocialNetworkAccountCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (!await personsAccountRepository.CheckIfAccountAddedByIdAsync(request.AccountId))
            throw new NotFound($"Account with id={request.AccountId} not found");

        var account = await personsAccountRepository.GetById(request.AccountId);

        if (account != null && !account.IsUserCreator(request.UserId))
            throw new Forbidden("You are not allowed to delete this account");

        if (account != null) personsAccountRepository.DeleteAsync(account);

        await personsAccountRepository.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}