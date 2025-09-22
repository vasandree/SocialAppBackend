using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccountModule.DataAccess.Interfaces.Repositories;
using SocialNetworkAccountModule.UseCases.Interfaces.Commands.PersonSocialNetworkAccount;
using SocialNetworkAccountModule.Domain.Entities;

namespace SocialNetworkAccountModule.UseCases.Implementation.Features.Commands.PersonsSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler(
    IPersonsAccountRepository personsAccountRepository)
    : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        if (await personsAccountRepository.CheckIfAccountIsAddedAsync(request.PersonId,
                request.SocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.SocialNetworkAccountDto.Type} account already added");

        await personsAccountRepository.AddAsync(new PersonsAccount(request.UserId,
            request.SocialNetworkAccountDto.Username, request.SocialNetworkAccountDto.Type, request.PersonId));

        await personsAccountRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}