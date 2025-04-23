using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    private readonly IPersonsAccountRepository _personsAccountRepository;

    public AddSocialNetworkAccountCommandHandler(IPersonsAccountRepository personsAccountRepository)
    {
        _personsAccountRepository = personsAccountRepository;
    }

    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        //todo: check if user exist
        
        //todo: check person existence

        if (await _personsAccountRepository.CheckIfAccountIsAddedAsync(request.UserId,
                request.SocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.SocialNetworkAccountDto.Type} account already added");


        await _personsAccountRepository.AddAsync(new PersonsAccount()
        {
            CreatorId = request.UserId,
            Username = request.SocialNetworkAccountDto.Username,
            Type = request.SocialNetworkAccountDto.Type,
            PersonsId = request.PersonId
        });

        return Unit.Value;
    }
}