using MediatR;
using Shared.Domain.Exceptions;
using SocialNetworkAccounts.Contracts.Commands.PersonSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;
using User.Contracts.Repositories;

namespace SocialNetworkAccounts.Application.Features.Commands.PersonsSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonsAccountRepository _personsAccountRepository;

    public AddSocialNetworkAccountCommandHandler(IPersonsAccountRepository personsAccountRepository,
        IUserRepository userRepository)
    {
        _personsAccountRepository = personsAccountRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
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