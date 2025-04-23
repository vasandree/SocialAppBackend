using Common.Exceptions;
using MediatR;
using SocialNetworkAccounts.Contracts.Commands.UserSocialNetworkAccount;
using SocialNetworkAccounts.Contracts.Repositories;
using SocialNetworkAccounts.Domain.Entities;

namespace SocialNetworkAccounts.Application.Features.Commands.UsersSocialNetworkAccount;

public class AddSocialNetworkAccountCommandHandler : IRequestHandler<AddSocialNetworkAccountCommand, Unit>
{
    private readonly IUsersAccountRepository _usersAccountRepository;

    public AddSocialNetworkAccountCommandHandler(IUsersAccountRepository usersAccountRepository)
    {
        _usersAccountRepository = usersAccountRepository;
    }


    public async Task<Unit> Handle(AddSocialNetworkAccountCommand request, CancellationToken cancellationToken)
    {
        //todo: check if id belongs tp user
        
        if (await _usersAccountRepository.CheckIfAccountIsAddedAsync(request.UserId,
                request.AddSocialNetworkAccountDto.Type))
            throw new BadRequest($"{request.AddSocialNetworkAccountDto.Type} account already added");
        
        //todo: check user existence

        await _usersAccountRepository.AddAsync(new UsersAccount()
        {
            UserId = request.UserId,
            Username = request.AddSocialNetworkAccountDto.Username,
            Type = request.AddSocialNetworkAccountDto.Type,
        });

        return Unit.Value;
    }
}